#nullable disable
namespace storeSPA.Data.Repos
{
    public class PerfumeRepo : IRepository<Perfume>
    {
        public async Task<ApiResult> Add(Perfume entity)
        {
            SqlConnection conn = GetConnectionString();
            string query = "AddPerfume";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", entity.Name);
            cmd.Parameters.AddWithValue("@img", entity.Image);
            cmd.Parameters.AddWithValue("@desc", entity.Description);
            cmd.Parameters.AddWithValue("@price", entity.Price);
            cmd.Parameters.AddWithValue("@quantity", entity.Quantity);
            cmd.Parameters.AddWithValue("@Saler_Id", entity.Saler_Id);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 0)
                    return new ApiResult { Result = true };
                return new ApiResult { Result = true };
            }
            catch (Exception)
            {
                return new ApiResult { Result = false, Errors = new () { "Un Expected Error"} };
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<ApiResult> Delete(String Id)
        {
            SqlConnection conn = GetConnectionString();
            try
            {
                string query = "DeletePerfume";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Id);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result <= 0)
                    return new ApiResult { Result = false, Errors = new () { "Operation Fail!"} };
                
                return new ApiResult { Result = true };
            }
            catch (Exception)
            {
                return new DataResult<Perfume> { Result = false, Errors = new () { "Un Expected Error" } };
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<DataResult<Perfume>> GetAll()
        {
            SqlConnection conn = GetConnectionString();
            string query = "GetAll";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Perfume> list = new List<Perfume>();

                while (reader.Read())
                {
                    Perfume perfume = new Perfume()
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Image = reader["Image"].ToString(),
                        Saler_Id = reader["Saler_Id"].ToString(),
                        Price = double.Parse(reader["Price"].ToString()),
                        Quantity = int.Parse(reader["Quantity"].ToString()),
                        Add_Date = DateTime.Parse(reader["Add_Date"].ToString())
                    };
                    list.Add(perfume);
                }
                return new DataResult<Perfume>
                {
                    Result = true,
                    Data = list
                };
            }
            catch (Exception)
            {
                return new DataResult<Perfume> { Result = false, Errors = new () { "Un Expected Error" } };
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<DataResult<Perfume>> GetAllForUser(string UserId)
        {
            SqlConnection conn = GetConnectionString();
            string query = "GetAllForUser";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", UserId);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Perfume> list = new ();

                while (reader.Read())
                {
                    Perfume perfume = new Perfume()
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Image = reader["Image"].ToString(),
                        Saler_Id = reader["Saler_Id"].ToString(),
                        Price = double.Parse(reader["Price"].ToString()),
                        Quantity = int.Parse(reader["Quantity"].ToString()),
                        Add_Date = DateTime.Parse(reader["Add_Date"].ToString())
                    };
                    list.Add(perfume);
                }
                return new DataResult<Perfume>
                {
                    Result = true,
                    Data = list
                };
            }
            catch (Exception)
            {
                return new DataResult<Perfume> { Result = false, Errors = new () { "Un Expected Error" } };
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<DataResult<Perfume>> GetById(string Id)
        {
            SqlConnection conn = GetConnectionString();
            string query = "GetById";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Perfume> list = new ();

                while (reader.Read())
                {
                    Perfume perfume = new Perfume()
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Image = reader["Image"].ToString(),
                        Saler_Id = reader["Saler_Id"].ToString(),
                        Price = double.Parse(reader["Price"].ToString()),
                        Quantity = int.Parse(reader["Quantity"].ToString()),
                        Add_Date = DateTime.Parse(reader["Add_Date"].ToString())
                    };
                    list.Add(perfume);
                }
                return new DataResult<Perfume>
                {
                    Result = true,
                    Data = list
                };
            }
            catch (Exception)
            {
                return new DataResult<Perfume> { Result = false, Errors = new () { "Un Expected Error" } };
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<DataResult<Perfume>> GetByName(string Name)
        {
            SqlConnection conn = GetConnectionString();
            string query = "GetByName";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", Name);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Perfume> list = new ();   

                while (reader.Read())
                {
                    Perfume perfume = new Perfume()
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Image = reader["Image"].ToString(),
                        Saler_Id = reader["Saler_Id"].ToString(),
                        Price = double.Parse(reader["Price"].ToString()),
                        Quantity = int.Parse(reader["Quantity"].ToString()),
                        Add_Date = DateTime.Parse(reader["Add_Date"].ToString())
                    };
                    list.Add(perfume);
                }
                return new DataResult<Perfume>
                {
                    Result = true,
                    Data = list
                };
            }
            catch (Exception)
            {
                return new DataResult<Perfume> { Result = false, Errors = new () { "Un Expected Error" } };
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<ApiResult> IsExists(string Id)
        {
            SqlConnection conn = GetConnectionString();
            string query = "IsExists";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (reader.Read())
                    if (int.Parse(reader[0].ToString()) == 1)
                        return new ApiResult { Result = true };

                return new ApiResult { Result = false };
            }
            catch (Exception)
            {
                return new DataResult<Perfume> { Result = false, Errors = new () { "Un Expected Error" } };
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<ApiResult> Update(Perfume entity)
        {

            SqlConnection conn = GetConnectionString();
            string query = "UpdatePerfume";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", entity.Id);
            cmd.Parameters.AddWithValue("@name", entity.Name);
            cmd.Parameters.AddWithValue("@img", entity.Image);
            cmd.Parameters.AddWithValue("@desc", entity.Description);
            cmd.Parameters.AddWithValue("@price", entity.Price);
            cmd.Parameters.AddWithValue("@quantity", entity.Quantity);
            cmd.Parameters.AddWithValue("@Saler_Id", entity.Saler_Id);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 0)
                    return new ApiResult { Result = true };
                return new ApiResult { Result = false, Errors = new List<string>() { "Operation Failed" } };
            }
            catch (Exception)
            {
                return new ApiResult { Result = false, Errors = new () { "Un Expected Error" } };
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
