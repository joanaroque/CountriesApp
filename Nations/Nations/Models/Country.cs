﻿namespace Nations.Models
{
    using System.Collections.Generic;

    using Nations.ViewModels;

    public class Country
    {
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public List<string> AltSpellings { get; set; }
        public double Area { get; set; }
        public List<string> Borders { get; set; }
        public List<string> CallingCodes { get; set; }
        public string Capital { get; set; }
        public string Cioc { get; set; }
        public List<Currency> Currencies { get; set; }
        public string Demonym { get; set; }
        public string Flag { get; set; }
        public double Gini { get; set; }
        public List<Language> Languages { get; set; }
        public List<double> Latlng { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string NumericCode { get; set; }
        public long Population { get; set; }
        public string Region { get; set; }
        public List<RegionalBloc> RegionalBlocs { get; set; }
        public string Subregion { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> TopLevelDomain { get; set; }
        public Translations Translations { get; set; }

        public Covid19Data Covid19Data { get; set; }
    }
}
