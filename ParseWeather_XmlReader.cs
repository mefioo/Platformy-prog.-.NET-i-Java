using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab01
{
    public class ParseWeather_XmlReader
    {
        public static WeatherData Parse(System.IO.Stream stream)
        {
            XmlTextReader reader = new XmlTextReader(stream);
            WeatherData result = new WeatherData()
            {
                City = string.Empty,
                Temperature = float.NaN
            };

            while (reader.Read())
            { 
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "city":
                                result.City = reader.GetAttribute("name");
                                break;
                            case "temperature":
                                result.Temperature = 
                                    float.Parse(
                                        reader.GetAttribute("value"),
                                        System.Globalization.CultureInfo.InvariantCulture);
                                break;
                                //Added description of weather to search in google, already replaceing spacebars with pluses.
                            case "weather":
                                result.description = reader.GetAttribute("value").ToString().Replace(" ", "+");
                                break;

                        }
                        break;
                }
            }
            if (result.City == string.Empty) { result.City = "city not found"; result.Temperature = 0; result.description = "error"; }
            return result;
        }
    }
}
