using FileImport.Models;
using System.Text.Json;
using System.Xml.Serialization;
using System.Text;


namespace FileImport.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private const string DataUrl = "https://data.cityofnewyork.us/resource/qz5f-yx82.csv";

        // Constructor to initialize the data file path
        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Load data from the CSV file
        public async Task<List<ZipCode>> LoadZipcodeDataAsync()
        {
            try 
            {
                var csvData = await _httpClient.GetStringAsync(DataUrl);
                var lines  = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var zipCodes = new List<ZipCode>();

                for (int i = 1; i < lines.Length; i++) 
                {
                    var fields = lines[i].Split(',');
                    var zipcode = new ZipCode
                    {
                        Oid = ParseInt(fields[0]),
                        ZipCodeValue = fields[1].Trim('"'),
                        HomeBroadbandAdoption = ParseDouble(fields[2]),
                        MobileBroadbandAdoption = ParseDouble(fields[3]),
                        NoInternetAccessPercentage = ParseDouble(fields[4]),
                        NoHomeBroadbandAdoption = ParseDouble(fields[5]),
                        NoMobileBroadbandAdoption = ParseDouble(fields[6]),
                        HomeBroadbandAdoptionCategory = fields[7].Trim('"'),
                        MobileBroadbandAdoptionCategory = fields[8].Trim('"'),
                        CommercialFiberMaxIsp = ParseInt(fields[9]),
                        PublicComputerCenterCount = ParseInt(fields[10]),
                        WorkstationsInPccs = ParseInt(fields[11]),
                        AvgTrainingHrsPerWeek = ParseDouble(fields[12]),
                        PublicWiFiCount = ParseInt(fields[13]),
                        PolesReservedByMobile = ParseInt(fields[14]),
                        PoleWithEquipmentInstalled = ParseInt(fields[15]),
                        DensityOfPolesReserved = ParseDouble(fields[16])
                    };
                    zipCodes.Add(zipcode);
                }
                return zipCodes;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error loading zip code data: {ex.Message}");
                return new List<ZipCode>();
           }
        }

        // Total number of zip codes in the CSV file
        public async Task<int> GetTotalZipcodesAsync()
        {
            try 
            {
                var csvData = await _httpClient.GetStringAsync(DataUrl);
                var lines  = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                return lines.Length - 1;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error loading zip code data: {ex.Message}");
                return 0;
            }
        }

        // Get zip codes where percentage of homes with no internet access is below a percentage 
        public async Task<List<ZipCode>> GetZipcodesWithNoInternetAccessBelowPercentageAsync(double percentage)
        {
            try
            {
                var zipCodes = await LoadZipcodeDataAsync();
                return zipCodes.Where(z => z.NoInternetAccessPercentage < percentage).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error filtering zip code data: {ex.Message}");
                return new List<ZipCode>();
            }
        }

        // Data Helpers 
        private int ParseInt(string value)
        {
            return int.Parse(value.Trim('"'));
        }

        private double ParseDouble(string value)
        {
            return double.Parse(value.Trim('"'));
        }

        // Export 
        public string ExportToJson(List<ZipCode> data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions{WriteIndented = true});
        }

        public string ExportToXml(List<ZipCode> data)
        {
            var serializer = new XmlSerializer(typeof(List<ZipCode>));

            using var writer = new StringWriter();

            serializer.Serialize(writer, data);

            return writer.ToString();
        }

        public string ExportToCsv(List<ZipCode> data)
        {
            var csv = new StringBuilder();

            csv.AppendLine(
                "OID,ZIP Code,Home Broadband Adoption,Mobile Broadband Adoption,No Internet Access Percentage,No Home Broadband Adoption," +
                "No Mobile Broadband Adoption,Home Broadband Adoption Category,Mobile Broadband Adoption Category,Commercial Fiber Max ISP," +
                "Public Computer Center Count,Workstations in PCCs,Avg Training Hrs Per Week,Public WiFi Count," +
                "Poles Reserved by Mobile,Pole with Equipment Installed,Density of Poles Reserved");

            foreach (var zipCode in data)
            {
                csv.AppendLine(
                    $"{zipCode.Oid}," +
                    $"{zipCode.ZipCodeValue}," +
                    $"{zipCode.HomeBroadbandAdoption}," +
                    $"{zipCode.MobileBroadbandAdoption}," +
                    $"{zipCode.NoInternetAccessPercentage}," +
                    $"{zipCode.NoHomeBroadbandAdoption}," +
                    $"{zipCode.NoMobileBroadbandAdoption}," +
                    $"\"{zipCode.HomeBroadbandAdoptionCategory}\"," +
                    $"\"{zipCode.MobileBroadbandAdoptionCategory}\"," +
                    $"{zipCode.CommercialFiberMaxIsp}," +
                    $"{zipCode.PublicComputerCenterCount}," +
                    $"{zipCode.WorkstationsInPccs}," +
                    $"{zipCode.AvgTrainingHrsPerWeek}," +
                    $"{zipCode.PublicWiFiCount}," +
                    $"{zipCode.PolesReservedByMobile}," +
                    $"{zipCode.PoleWithEquipmentInstalled}," +
                    $"{zipCode.DensityOfPolesReserved}");
            }

            return csv.ToString();
        }
    }
}