using System.Text;

namespace ConsoleApp1
{
  public class XmlParser
  {
    private readonly string? xml;  // Create a field
    private XmlState currentState = XmlState.ClosingTagEnded;
    private readonly StringBuilder currentText = new StringBuilder();
    private readonly Stack<string> XmlOpenTags = new Stack<string>();

  // Create a class constructor for the Car class
    public XmlParser(string? xml)
    {
      this.xml = xml;
    }

    public bool DetermineXml()
    {
      if (xml == null || xml.Length == 0)
        throw new InvalidDataException(Constants.XML_EMPTY);

      char[] charArray = xml.ToCharArray();
      for(int i = 0; i < charArray.Length; i++) 
      {
        switch(charArray[i]) 
        {
          case '<':
            bool nextCharExists = i + 1 < charArray.Length;
            if(nextCharExists && charArray[i + 1].Equals('/')) {
              currentState = XmlState.ClosingTagStarted;
              processClosingTagStart();
              //ignore next character (which is /)
              i++;
            } else {
              currentState = XmlState.OpeningTagStarted;
            }
            break;
          case '>':
            if(currentState == XmlState.OpeningTagStarted) {
              processOpeningTagEnd();
              currentState = XmlState.OpeningTagEnded;
            } else if (currentState == XmlState.ClosingTagStarted) {
              processClosingTagEnd();
              currentState = XmlState.ClosingTagEnded;
            } else {
              throw new InvalidDataException($"Unexpected > in XML at {i}");
            }
            break;
          default:
            if(currentState == XmlState.OpeningTagStarted || currentState == XmlState.ClosingTagStarted) {
              processTagName(charArray[i]);
            } else if (currentState == XmlState.OpeningTagEnded) {
              processTagValue(charArray[i]);
            } else {
              throw new InvalidDataException($"Unexpected character '{charArray[i]}' in XML at {i}");
            }
            break;
        }
      }
      return currentState == XmlState.ClosingTagEnded;
    }

    private void processTagValue(char ch) {
      currentText.Append(ch);
    }

    private void processTagName(char ch) {
      currentText.Append(ch);
    }

    private void processOpeningTagEnd() {
      XmlOpenTags.Push(currentText.ToString());
      currentText.Clear();
    }

    private void processClosingTagEnd() {
      string closingTagText = currentText.ToString();
      string openingTagText = XmlOpenTags.Pop();
      if (!closingTagText.Equals(openingTagText)) {
        throw new InvalidDataException($"Incorrect closing tag <{closingTagText}> for opening tag <{openingTagText}> in XML");
      }
      currentText.Clear();
    }

    private void processClosingTagStart() {
      //Tag content should be handled here...
      currentText.Clear();
    }
  }
}