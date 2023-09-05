
using System.Data.SqlClient;
using System.Management;
using System.Runtime.Versioning;

namespace pcinfo4
{
    [SupportedOSPlatform("windows")]
    class conexiondb
    {        
        string chainConexion = "Data Source=.;user id=sa;password=<YourStrong@Passw0rd>;Initial Catalog=test";
        public SqlConnection conn = new SqlConnection();

        public conexiondb()
        {
            conn.ConnectionString = chainConexion;
        }

        public void openConn()
        {
            try
            {
                conn.Open();
                Console.WriteLine("Conexion is open");
            }
            catch (Exception e)
            {
                Console.WriteLine("Conexion error: " + e.Message);
            }
        }

        public void closeConn()
        {
            Console.WriteLine("Conexion close");
            conn.Close();

        }

        static void pcData()
        {
            string serialMB = string.Empty;
            string manufacurerMB = string.Empty;
            string productMB = string.Empty;

            string nameSO = string.Empty;
            string archSO = string.Empty;
            string organizationSO = string.Empty;
            string versionSO = string.Empty;
            string serialNumberSO = string.Empty;

            string sizeStorage = string.Empty;
            string BrandStorage = string.Empty;
            string modelStorage = string.Empty;
            string serialNumberStorage = string.Empty;

            string machineName = string.Empty;
            string modelSdd = string.Empty;
            string numberSerialSdd = string.Empty;

            string memoryRam = string.Empty;
            string brandRam = string.Empty;
            string numberSerialRam  = string.Empty;

            //MotherBoard
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                try
                {
                    serialMB = mo.GetPropertyValue("SerialNumber").ToString();
                    manufacurerMB = mo.GetPropertyValue("Manufacturer").ToString();
                    productMB =  mo.GetPropertyValue("Product").ToString();
                }
                catch
                { }
            }

            //S.O.
            ManagementObjectSearcher so = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject sys in so.Get())
            {
                try
                {
                    nameSO = sys.GetPropertyValue("Name").ToString();
                    versionSO = sys.GetPropertyValue("Version").ToString();
                    organizationSO = sys.GetPropertyValue("Manufacturer").ToString();//
                    archSO = sys.GetPropertyValue("OSArchitecture").ToString();
                    serialNumberSO = sys.GetPropertyValue("SerialNumber").ToString();
                }
                catch
                { }
            }

            //SDDinfo
            ManagementObjectSearcher disks = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            
            foreach (ManagementObject disk in disks.Get())
            {                
                machineName = disk.GetPropertyValue("SystemName").ToString();
                modelSdd = disk.GetPropertyValue("Model").ToString();
                numberSerialSdd = disk.GetPropertyValue("SerialNumber").ToString();

                long hddSizeBytes = Int64.Parse(disk["Size"].ToString());
                double hddSizeGBytes = hddSizeBytes / 1024 / 1024 / 1024;
                sizeStorage = (hddSizeGBytes + " GB");               
            }

            //Processor
            ManagementObjectSearcher pros = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor ");
            foreach (ManagementObject pro in pros.Get())
            {
                try
                {
                    Console.WriteLine(pro.GetPropertyValue("Name").ToString());
                    Console.WriteLine(pro.GetPropertyValue("Manufacturer").ToString());
                    Console.WriteLine(pro.GetPropertyValue("NumberOfCores").ToString());
                    Console.WriteLine(pro.GetPropertyValue("ProcessorId").ToString());
                    Console.WriteLine(pro.GetPropertyValue("Role").ToString());
                    Console.WriteLine("");
                }
                catch
                { }
            }

            //Processor
            ManagementObjectSearcher keys = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM SoftwareLicensingService ");
            foreach (ManagementObject key in keys.Get())
            {
                try
                {                    
                    Console.WriteLine(key.GetPropertyValue("OA3xOriginalProductKey").ToString());                    
                    Console.WriteLine("");
                }
                catch
                { }
            }

            //RAM
            ManagementObjectSearcher rams = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject ram in rams.Get())
            {
                var capacity = Convert.ToUInt64(ram.Properties["Capacity"].Value);
                var capacityKB = capacity / 1024;
                var capacityMB = capacityKB / 1024;
                memoryRam = (capacityMB + "MB");
                brandRam = ram.GetPropertyValue("Manufacturer").ToString();
                numberSerialRam = ram.GetPropertyValue("SerialNumber").ToString();

                Console.WriteLine(memoryRam);
                Console.WriteLine(brandRam);
                Console.WriteLine(numberSerialRam);
            }

            Console.WriteLine("");

            Console.WriteLine(machineName);

            Console.WriteLine(manufacurerMB);
            Console.WriteLine(serialMB);
            Console.WriteLine(productMB);            
            Console.WriteLine("");

            Console.WriteLine(organizationSO);
            Console.WriteLine(nameSO);
            Console.WriteLine(versionSO);            
            Console.WriteLine(archSO);
            Console.WriteLine(serialNumberSO);
            Console.WriteLine("");

            Console.WriteLine(modelSdd);
            Console.WriteLine(sizeStorage);
            Console.WriteLine(numberSerialSdd);
                       

        }

        static void insertData()
        {
            conexiondb conexion = new conexiondb();
            conexion.openConn();
            conexion.closeConn();
            pcData();
        }

        static void Main()
        {
            insertData();
        }

    }

}
