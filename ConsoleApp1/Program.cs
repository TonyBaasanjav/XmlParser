using ConsoleApp1;

String? xml = Console.ReadLine();

XmlParser xmlParser = new XmlParser(xml);

bool isValidXml = false;
try {
  isValidXml = xmlParser.DetermineXml();
} catch(Exception e) {
  Console.WriteLine(e.Message);
}

Console.WriteLine(isValidXml);
