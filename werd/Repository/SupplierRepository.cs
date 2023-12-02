using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Numerics;
using werd.Model;

namespace werd.Repository
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private readonly DBContext context;
        public SupplierRepository(DBContext context)
        {
            this.context = context;
        }

        public string Add(Supplier entity)
        {
            using (var conn = this.context.CreateConnection())
            {
                string sql = "SELECT werd.get_supplier_id()";
                entity.Id = conn.QueryFirstOrDefault<string>(sql);
                sql = @$"
	                    INSERT INTO werd.tb_supplier(
	                        create_time, creator, update_time, updater, remark, is_deleted, id, supplier,
	                        unified_business_no, email,address_1, address_2, head, head_phone_1, head_phone_2,
	                        contact_person, contact_person_phone_1,contact_person_phone_2, other_contact_1,
	                        other_contact_2)
	                    VALUES (now(),@creator,now(),@updater,@remark,false,@id,@suppliername,@unifiedbusinessno,@email,@address1, @address2, @head,@headphone1,@headphone2,@contactperson,@contactpersonphone1,@contactpersonphone2,@othercontact1,
	                        @othercontact2)
	                    RETURNING id;";

                return conn.QueryFirstOrDefault<string>(sql, entity);
            };
        }

        public bool Delete(string[] ids)
        {
            using (var conn = this.context.CreateConnection())
            {
                string sql = $@"
                        UPDATE werd.tb_supplier
                        SET update_time = now(),
		                    updater = updater,
		                    is_deleted = true
                        WHERE id = ANY(@ids)";

                if (conn.Execute(sql, new
                {
                    ids = ids
                }) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public (List<Supplier> suppliers, int total) GetPagination(SearchCondition searchCondition)
        {
            using (var conn = this.context.CreateConnection())
            {
                string sql = @"
                    SELECT 
                        creator, updater, remark, id, supplier as suppliername,
                        unified_business_no as unifiedbusinessno,
                        email, address_1 as address1, address_2 as address2,
                        head,
                        head_phone_1 as headphone1,
                        head_phone_2 as headphone2,
                        contact_person_phone_1 as contactpersonhone1,
                        contact_person_phone_2 as contactpersonhone2,
                        head_phone_1 as headphone1,
                        head_phone_2 as headphone2,
                        contact_person as contactperson,
                        contact_person_phone_1 as contactpersonphone1,
                        contact_person_phone_2 as contactpersonphone2,
                        other_contact_1 as othercontact1,
                        other_contact_2 as othercontact2

                    FROM werd.tb_supplier
                    WHERE
                        is_deleted = false
                        AND (@supplier IS NULL OR supplier LIKE '%' || @supplier || '%')
                        AND (@unified_business_no IS NULL OR unified_business_no LIKE '%' || @unified_business_no || '%')
                        AND (@address IS NULL OR (address_1 LIKE '%' || @address || '%' OR address_2 LIKE '%' || @address || '%'))
                        AND (@email IS NULL OR email LIKE '%' || @email || '%')
                        AND (@name IS NULL OR (head LIKE '%' || @name || '%' OR contact_person LIKE '%' || @name || '%'))
                        AND (@phone IS NULL OR (head_phone_1 LIKE '%' || @phone || '%' OR head_phone_2 LIKE '%' || @phone || '%' OR contact_person_phone_1 LIKE '%' || @phone || '%' OR contact_person_phone_2 LIKE '%' || @phone || '%'))
                    ORDER BY create_time
                    OFFSET @i_page
                    FETCH NEXT @pageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM werd.tb_supplier
                    WHERE
                        is_deleted = false
                        AND (@supplier IS NULL OR supplier LIKE '%' || @supplier || '%')
                        AND (@unified_business_no IS NULL OR unified_business_no LIKE '%' || @unified_business_no || '%')
                        AND (@address IS NULL OR (address_1 LIKE '%' || @address || '%' OR address_2 LIKE '%' || @address || '%'))
                        AND (@email IS NULL OR email LIKE '%' || @email || '%')
                        AND (@name IS NULL OR (head LIKE '%' || @name || '%' OR contact_person LIKE '%' || @name || '%'))
                        AND (@phone IS NULL OR (head_phone_1 LIKE '%' || @phone || '%' OR head_phone_2 LIKE '%' || @phone || '%' OR contact_person_phone_1 LIKE '%' || @phone || '%' OR contact_person_phone_2 LIKE '%' || @phone || '%'));";

                var parameters = new
                {
                    supplier = searchCondition.Supplier,
                    unified_business_no = searchCondition.UnifiedBusinessNo,
                    address = searchCondition.Address,
                    email = searchCondition.Email,
                    name = searchCondition.Name,
                    phone = searchCondition.Phone,
                    i_page = (searchCondition.Page - 1) * searchCondition.PageSize,
                    pageSize = searchCondition.PageSize
                };

                using (var multi = conn.QueryMultiple(sql, parameters))
                {
                    List<Supplier> suppliers = multi.Read<Supplier>().ToList();
                    int total = multi.Read<int>().FirstOrDefault();

                    return (suppliers, total);
                }
            };
        }

        public int Update(Supplier entity)
        {
            using (var conn = this.context.CreateConnection())
            {
                string sql = @"
                        UPDATE werd.tb_supplier
                        SET 
                            update_time=now(),
		                    updater = @updater,
		                    remark = @remark,
		                    supplier = @suppliername,
		                    unified_business_no = @unifiedbusinessno,
		                    email = @email,
		                    address_1 = @address1,
		                    address_2 = @address2,
		                    head = @head,
		                    head_phone_1 = @headphone1,
		                    head_phone_2 = @headphone2,
		                    contact_person = @contactperson,
		                    contact_person_phone_1 = @contactpersonphone1,
		                    contact_person_phone_2 = @contactpersonphone2,
		                    other_contact_1 = @othercontact1,
		                    other_contact_2 = @othercontact2
	                    WHERE id = @id;";

                return conn.Execute(sql, entity);
            }
        }

        int IRepository<Supplier>.Add(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Supplier> GetAll()
        {
            throw new NotImplementedException();
        }

        public Supplier GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
