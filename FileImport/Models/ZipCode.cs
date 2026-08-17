namespace FileImport.Models
{
    public class ZipCode
    {
        public int Oid { get; set; }

        public string? ZipCodeValue { get; set; }

        public double HomeBroadbandAdoption { get; set; }

        public double MobileBroadbandAdoption { get; set; }

        public double NoInternetAccessPercentage { get; set; }

        public double NoHomeBroadbandAdoption { get; set; }

        public double NoMobileBroadbandAdoption { get; set; }

        public string? HomeBroadbandAdoptionCategory { get; set; }

        public string? MobileBroadbandAdoptionCategory { get; set; }

        public int CommercialFiberMaxIsp { get; set; }

        public int PublicComputerCenterCount { get; set; }

        public int WorkstationsInPccs { get; set; }

        public double AvgTrainingHrsPerWeek { get; set; }

        public int PublicWiFiCount { get; set; }

        public int PolesReservedByMobile { get; set; }

        public int PoleWithEquipmentInstalled { get; set; }

        public double DensityOfPolesReserved { get; set; }

    }
}