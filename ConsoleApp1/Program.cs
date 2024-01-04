using ConsoleApp1;

Console.WriteLine("Enter XML:");
string? xml = Console.ReadLine();

XmlParser xmlParser = new XmlParser(xml);

bool isValidXml = false;
try {
  isValidXml = xmlParser.DetermineXml();
} catch(Exception e) {
  Console.WriteLine(e.Message);
}

Console.WriteLine(isValidXml);
