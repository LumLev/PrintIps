using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;


foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
{
    Console.WriteLine("========================================================");
    Console.WriteLine("========================================================");
    IPInterfaceProperties properties = networkInterface.GetIPProperties();
    Console.WriteLine($"DNS Suffix: {properties.DnsSuffix}");

    #region IPV4
        var v4props = properties.GetIPv4Properties();
        Console.WriteLine($"IPV4 = index: {v4props.Index}; MTU: {v4props.Mtu}, Auto Private Address:");
        if (OperatingSystem.IsLinux() || OperatingSystem.IsWindows())
    {
      
#pragma warning disable CA1416 // Validate platform compatibility
        Console.WriteLine($" (enabled:{v4props.IsAutomaticPrivateAddressingEnabled}) (active:{v4props.IsAutomaticPrivateAddressingActive}); \r\n " +
            $"DHCP: {v4props.IsDhcpEnabled}; Forwarding: {v4props.IsForwardingEnabled}; Auto Private");
#pragma warning restore CA1416 // Validate platform compatibility
    }
    #endregion

    #region IPV6
    var v6props = properties.GetIPv6Properties();
    Console.WriteLine($"IPV4 = index: {v4props.Index}; MTU: {v4props.Mtu}, Auto Private Address:");
    if (OperatingSystem.IsLinux() || OperatingSystem.IsWindows())
    {

#pragma warning disable CA1416 // Validate platform compatibility
        Console.WriteLine($" (enabled:{v4props.IsAutomaticPrivateAddressingEnabled}) (active:{v4props.IsAutomaticPrivateAddressingActive}); \r\n " +
            $"DHCP: {v4props.IsDhcpEnabled}; Forwarding: {v4props.IsForwardingEnabled}; Auto Private");
#pragma warning restore CA1416 // Validate platform compatibility
    }
    #endregion


    Console.WriteLine("\r\n========================================================");
    Console.WriteLine("Multicast Addresses:");
   foreach(var multicast in properties.MulticastAddresses)
    {
        Console.WriteLine(multicast.Address.ToString());
    }

    Console.WriteLine("========================================================");
    Console.WriteLine("Dns Addresses:"); 
    foreach(var dnsaddress in properties.DnsAddresses)
    {
        if (dnsaddress is not null)
        {
            Console.WriteLine(dnsaddress.ToString());
        }
    }
    Console.WriteLine("========================================================");

    Console.WriteLine("Unicast Addresses");
    foreach (UnicastIPAddressInformation address in properties.UnicastAddresses)
    {
        Console.WriteLine(address.Address.ToString());
    }

    Console.WriteLine("\r\n========================================================");
    Console.WriteLine("Gateway Addresses");
    foreach (var address in properties.GatewayAddresses)
    {
        Console.WriteLine(address.Address.ToString());
    }

    Console.WriteLine("\r\n========================================================");
    if (OperatingSystem.IsMacOS() is false && OperatingSystem.IsIOS() is false)
    {
#pragma warning disable CA1416 // Validate platform compatibility
        Console.WriteLine($"Dns Enabled: {properties.IsDnsEnabled}, Dynamic Dns Enabled: {properties.IsDynamicDnsEnabled}");
        Console.WriteLine($"DHCP Server: {(properties.DhcpServerAddresses.Count > 0)}");
        Console.WriteLine("\r\n========================================================");
        var dhcpServerAddresses = properties.DhcpServerAddresses;
#pragma warning restore CA1416 // Validate platform compatibility
        if (dhcpServerAddresses is not null)
        {
            foreach(var dhcpaddress in dhcpServerAddresses)
            {
                Console.WriteLine(dhcpaddress.GetAddressBytes());
            }
        }
    }
    Console.WriteLine("========================================================");
    Console.WriteLine("========================================================");
}



