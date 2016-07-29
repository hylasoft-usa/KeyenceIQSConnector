using System;
using System.Configuration;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Config
{
  public class ConfigurationReader : IConfigurationReader
  {
    protected const int DefaultPort = 55055;
    protected const string DefaultIp = "127.0.0.1";

    protected const string PortConfigName = "Port";
    protected const string HostConfigName = "HostIp";

    public SimulationConfig Read()
    {
      var port = GetConfigurationInt(PortConfigName, DefaultPort);
      var ip = GetConfigurationString(HostConfigName, DefaultIp);

      return new SimulationConfig(port, ip);
    }

    protected string GetConfigurationString(string configName, string defaultValue)
    {
      return TryGetConfigurationValue(configName, val => val, defaultValue);
    }

    protected int GetConfigurationInt(string configName, int defaultValue)
    {
      return TryGetConfigurationValue(configName, val => RetrieveInt(val, defaultValue), defaultValue);
    }

    protected TVal TryGetConfigurationValue<TVal>(string configName, Func<string, TVal> parse, TVal defaultValue)
    {
      try
      {
        var configVal = ConfigurationManager.AppSettings[configName];
        return string.IsNullOrEmpty(configVal) ? defaultValue : parse(configVal);
      }
      catch
      {
        return defaultValue;
      }
    }

    protected string RetrieveString(string configVal)
    {
      return string.IsNullOrEmpty(configVal)
        ? string.Empty
        : configVal.Trim();
    }

    protected int RetrieveInt(string configVal, int defualt)
    {
      int parsedVal;
      return (string.IsNullOrEmpty(configVal) || !int.TryParse(configVal, out parsedVal))
        ? defualt
        : parsedVal;
    }
  }
}
