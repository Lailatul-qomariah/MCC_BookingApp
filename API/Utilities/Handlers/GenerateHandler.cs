using API.Data;
using API.Models;

namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {
        
        public static string GenerateNik(Employee employee)
        {
            if (employee is null)
            {
                // Jika ada data sebelumnya, tambahkan 1 ke NIK terakhir
                int lastNik = int.Parse(employee.Nik);
                lastNik++;
                return lastNik.ToString();  //mengubah objek ke bentuk string
            }
            else
            {
                // Jika tidak ada data employee sebelumnya, maka returnnya berupa angka 111111
                return "111111";
            }
        }

    }
}
