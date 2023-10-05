using API.Data;
using API.Models;

namespace API.Utilities.Handlers;

public class GenerateHandler
{

    public static string GenerateNik(Employee employee)
    {
        if (employee is null) // cek apakah employee kosong
        {
            // Jika tidak ada data employee sebelumnya, maka returnnya berupa angka 111111
            return "111111";
        }
        else
        {
            // Jika ada data sebelumnya, tambahkan 1 ke Nik terakhir
            int lastNik = int.Parse(employee.Nik);
            lastNik++;
            return lastNik.ToString();  //mengubah objek ke bentuk string
        }
    }


    public static int GenerateOtp()
    {
        Random random = new Random();
        int otp = random.Next(100000, 999999);
        return otp;
    }
}


