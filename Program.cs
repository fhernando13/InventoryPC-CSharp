
using System.ComponentModel;
using System.Data.SqlClient;
using System.Management;
using System.Runtime.Versioning;

namespace pcinfo4
{
    [SupportedOSPlatform("windows")]
    class conexiondb
    {
        public string chainConexion = "Data Source=.;user id=sa;password=<YourStrong@Passw0rd>;Initial Catalog=pcinfo";
        public SqlConnection conn = new SqlConnection();
        public SqlCommand cmd;

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
            int counter = 0;
            int counterssd = 0;
            string? serialMB = "";
            string? manufacurerMB ="";
            string? productMB = "";

            string? nameSO = "";
            string? archSO = "";
            string? organizationSO = "";
            string? versionSO = "";
            string? serialNumberSO = "";
            
            string? sizeStorage = "";
            string? machineName = "";
            string? modelSdd = "";
            string? numberSerialSdd = "";

            string? keyPc = "";
            
            string? brandRam = "";
            string? numberSerialRam  = "";

            string? NamePro = "";
            string? Manufacturer = "";
            string? NumberOfCores = "";
            string? ProcessorId = "";
            string? Role = "";

            string sql = "";

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
                catch(Exception e)
                {
                    Console.WriteLine("Error:"+e.Message);
                }
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
                catch(Exception e)
                {
                    Console.WriteLine("Error:"+e.Message);
                }
            }

            //SDDinfo
            ManagementObjectSearcher disks = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            Dictionary<string, string> ssds = new Dictionary<string, string>()
            {

            };

            foreach (ManagementObject disk in disks.Get())
            {           
                counterssd = counterssd +1; 
                machineName = disk.GetPropertyValue("SystemName").ToString();
                modelSdd = disk.GetPropertyValue("Model").ToString();
                numberSerialSdd = disk.GetPropertyValue("SerialNumber").ToString();                    
                long? hddSizeBytes = Int64.Parse(disk["Size"].ToString());
                sizeStorage = hddSizeBytes / 1024 / 1024 / 1024+" GB"; 

                ssds.Add($"Model storage {counterssd}",modelSdd);
                ssds.Add($"Size storage {counterssd}",sizeStorage);
                ssds.Add($"Number serie storage {counterssd}",numberSerialSdd);

            }

            //Processor
            ManagementObjectSearcher pros = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor ");
            foreach (ManagementObject pro in pros.Get())
            {
                try
                {
                    NamePro = pro.GetPropertyValue("Name").ToString();
                    Manufacturer = pro.GetPropertyValue("Manufacturer").ToString();
                    NumberOfCores = pro.GetPropertyValue("NumberOfCores").ToString();
                    ProcessorId = pro.GetPropertyValue("ProcessorId").ToString();
                    Role = pro.GetPropertyValue("Role").ToString();                    
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error:"+e.Message);
                }
            }

            //Key Pc
            ManagementObjectSearcher keys = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM SoftwareLicensingService ");
            foreach (ManagementObject key in keys.Get())
            {
                try
                {
                    keyPc = key.GetPropertyValue("OA3xOriginalProductKey").ToString();                    
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error:"+e.Message);
                }
            }

            //RAM
            ManagementObjectSearcher rams = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            Dictionary<string, string> Slots = new Dictionary<string, string>()
            {
                {"Brand memory 1", "void"},
                {"Number serial 1", "void"},
                {"Slot 1 capacity MB", "0"},
                {"Brand memory 2", "void"},
                {"Number serial 2", "void"},
                {"Slot 2 capacity MB", "0"},
                {"Brand memory 3", "void"},
                {"Number serial 3", "void"},
                {"Slot 3 capacity MB", "0"},
                {"Brand memory 4", "void"},
                {"Number serial 4", "void"},
                {"Slot 4 capacity MB", "0"},
            };
            foreach (ManagementObject ram in rams.Get())
            {
                try
                {
                    counter = counter +1;
                    var capacity = Convert.ToUInt64(ram.Properties["Capacity"].Value);
                    // var capacityKB = capacity / 1024;
                    var capacityMB = capacity / 1024 /1024;

                    brandRam = ram.GetPropertyValue("Manufacturer").ToString();
                    numberSerialRam = ram.GetPropertyValue("SerialNumber").ToString();

                    //Brand
                    if(Slots.ContainsKey($"Brand memory {counter}"))        
                        Slots[$"Brand memory {counter}"] = brandRam; 
                    if(Slots.ContainsKey($"Brand memory {counter}"))        
                        Slots[$"Brand memory {counter}"] = brandRam; 
                    if(Slots.ContainsKey($"Brand memory {counter}"))        
                        Slots[$"Brand memory {counter}"] = brandRam;
                    if(Slots.ContainsKey($"Brand memory {counter}"))        
                        Slots[$"Brand memory {counter}"] = brandRam;
                    //NO. Serie
                    if(Slots.ContainsKey($"Number serial {counter}"))        
                        Slots[$"Number serial {counter}"] = numberSerialRam;
                    if(Slots.ContainsKey($"Number serial {counter}"))        
                        Slots[$"Number serial {counter}"] = numberSerialRam;
                    if(Slots.ContainsKey($"Number serial {counter}"))        
                        Slots[$"Number serial {counter}"] = numberSerialRam;
                    if(Slots.ContainsKey($"Number serial {counter}"))        
                        Slots[$"Number serial {counter}"] = numberSerialRam;
                    //Capacity
                    if(Slots.ContainsKey($"Slot {counter} capacityMB"))        
                        Slots[$"Slot {counter} capacityMB"] = capacityMB.ToString();
                    if(Slots.ContainsKey($"Slot {counter} capacityMB"))        
                        Slots[$"Slot {counter} capacityMB"] = capacityMB.ToString();
                    if(Slots.ContainsKey($"Slot {counter} capacityMB"))        
                        Slots[$"Slot {counter} capacityMB"] = capacityMB.ToString();
                    if(Slots.ContainsKey($"Slot {counter} capacityMB"))        
                        Slots[$"Slot {counter} capacityMB"] = capacityMB.ToString();

                }
                catch(Exception e)
                {
                    Console.WriteLine("Error:"+e.Message);
                }
            }    
                                                                     

            // Console.WriteLine("Name machine: "+machineName);
            // Console.WriteLine("\n*** Description MotherBoard ***");
            // Console.WriteLine("Brand: "+manufacurerMB);
            // Console.WriteLine("Number serie: "+serialMB);
            // Console.WriteLine("Model: "+productMB);
            // Console.WriteLine("\n*** Description S.O. ***");
            // Console.WriteLine("Corporation: "+organizationSO);
            // Console.WriteLine("NameS.O.: "+nameSO);
            // Console.WriteLine("Version S.O.: "+versionSO);
            // Console.WriteLine("Architecture: "+archSO);
            // Console.WriteLine("Number serie S.O.: "+serialNumberSO);
            // Console.WriteLine("Key activation: "+keyPc);
            // Console.WriteLine("\n*** Description Processor ***");
            // Console.WriteLine("Nme processor: "+NamePro);
            // Console.WriteLine("Brand processor: "+Manufacturer);
            // Console.WriteLine("Number cores: "+NumberOfCores);
            // Console.WriteLine("Id processor: "+ProcessorId);
            // Console.WriteLine("Role Processor: "+Role);
            // Console.WriteLine("\n*** Description SSD ***");
            // Console.WriteLine(ssds["Model storage 1"]);
            // Console.WriteLine(ssds["Size storage 1"]);
            // Console.WriteLine(ssds["Number serie storage 1"]);
            // Console.WriteLine("\n*** Description RAM ***");
            // foreach(var slot in Slots)
            // {
            //     Console.WriteLine("{0} : {1}",slot.Key,slot.Value);                                    
            // }


            conexiondb conexion = new conexiondb();
            conexion.openConn();
            try
            {
                conexion.cmd = new SqlCommand($"INSERT INTO dbo.pc( MachineName, BrandMB, ModelMB, NoSerieMB, CorporationSO, NameSO, VersionSO, archSO, NumberSerialSO, KeyActivation, NameProcessor, ManufacturerProcessor, NumberOfCores, RoleProcessor, ProcessorId, ModelSSD, SizeSSD, NumberSerialSSD, SlotOneBrandRam, SlotOneNumberSerialRam, SlotOneStorageRam, SlotTwoBrandRam, SlotTwoNumberSerialRam, SlotTwoStorageRam, SlotTreeBrandRam, SlotTreeNumberSerialRam, SlotTreeStorageRam, SlotFourBrandRam, SlotFourNumberSerialRam, SlotFourStorageRam) VALUES ( '{machineName}', '{manufacurerMB}', '{productMB}', '{serialMB}', '{organizationSO}', '{nameSO}', '{versionSO}', '{archSO}', '{serialNumberSO}', '{keyPc}', '{NamePro}', '{Manufacturer}', '{NumberOfCores}', '{Role}', '{ProcessorId}', '{ssds["Model storage 1"]}', '{ssds["Size storage 1"]}', '{ssds["Number serie storage 1"]}', '{Slots["Brand memory 1"]}', '{Slots["Slot 1 capacity MB"]}', '{Slots["Number serial 1"]}', '{Slots["Brand memory 2"]}', '{Slots["Slot 2 capacity MB"]}', '{Slots["Number serial 2"]}', '{Slots["Brand memory 3"]}', '{Slots["Slot 3 capacity MB"]}', '{Slots["Number serial 3"]}', '{Slots["Brand memory 4"]}', '{Slots["Slot 4 capacity MB"]}', '{Slots["Number serial 4"]}')");
                 conexion.cmd.ExecuteNonQuery();
                Console.WriteLine("Registre PC");
                conexion.closeConn();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: "+e.Message);
            }
            
        }

        static void insertData()
        {
            conexiondb conexion = new conexiondb();
            conexion.openConn();
            //insert pcinfo (name,name,name,name,name) in (1,2,3,4,5);
            conexion.closeConn();
            pcData();
        }

        static void Main()
        {
            // insertData();
            pcData();
        }

    }

}
