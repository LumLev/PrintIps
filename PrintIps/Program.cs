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


    Console.WriteLine("\r\n========================================================");
    Console.WriteLine("Multicast Addresses:");
   foreach(var multicast in properties.MulticastAddresses)
    {
        if (multicast is not null)
        {
            Console.WriteLine(multicast.Address.ToString());

        }
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



