namespace ConsoleApp1
{
  public class XmlParser
  {
    private string? xml;  // Create a field

  // Create a class constructor for the Car class
    public XmlParser(string? xml)
    {
      this.xml = xml;
    }

    public bool DetermineXml()
    {
      if (xml == null)
        throw new InvalidDataException(Constants.XML_EMPTY);
      return false;
    }
  }
}