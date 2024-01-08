using System.Text;

namespace ConsoleApp1
{
  public class XmlParser
  {
    public readonly char[] charArray;
    public XmlState currentState = XmlState.ClosingTagEnded;
    public readonly StringBuilder currentText = new StringBuilder();
    public readonly Stack<string> XmlOpenTags = new Stack<string>();
    private TagOpeningHandler tagOpeningHandler;
    private TagClosingHandler tagClosingHandler;
    private CharHandler charHandler;

  // Create a class constructor for the Car class
    public XmlParser(string? xml)
    {
      if (xml == null || xml.Length == 0 || xml.Trim().Length == 0)
        throw new InvalidDataException(Constants.XML_EMPTY);
      charArray = xml.Trim().ToCharArray();
      tagOpeningHandler = new TagOpeningHandler(this);
      tagClosingHandler = new TagClosingHandler(this);
      charHandler = new CharHandler(this);
    }

    public bool DetermineXml()
    {
      for(int i = 0; i < charArray.Length; i++) 
      {
        ICharHandler handler = determineInstance(charArray[i]);
        int skipTo = handler.handle(i);
        if(skipTo != i)
          i = skipTo;
      }
      return currentState == XmlState.ClosingTagEnded;
    }

    public ICharHandler determineInstance(char ch) {
      switch(ch) 
        {
          case '<': return tagOpeningHandler;
          case '>': return tagClosingHandler;
          default: return charHandler;
        }
    }
  }
}