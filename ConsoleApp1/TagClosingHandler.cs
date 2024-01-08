
namespace ConsoleApp1 {

    public class TagClosingHandler : ICharHandler
    {
        
        private XmlParser xmlParser;

        public TagClosingHandler(XmlParser xmlParser) {
            this.xmlParser = xmlParser;
        }

        public int handle(int currentIndex)
        {
            if(xmlParser.currentState == XmlState.OpeningTagStarted) {
                if(xmlParser.currentText.ToString().Length == 0) {
                    throw new InvalidDataException($"Tag name should not be empty at in XML at {currentIndex}");
                }
                processOpeningTagEnd();
                xmlParser.currentState = XmlState.OpeningTagEnded;
            } else if (xmlParser.currentState == XmlState.ClosingTagStarted) {
              processClosingTagEnd();
              xmlParser.currentState = XmlState.ClosingTagEnded;
            } else {
              throw new InvalidDataException($"Unexpected > in XML at {currentIndex}");
            }
            return currentIndex;
        }

        private void processOpeningTagEnd() {
            xmlParser.XmlOpenTags.Push(xmlParser.currentText.ToString());
            xmlParser.currentText.Clear();
        }

        private void processClosingTagEnd() {
            string closingTagText = xmlParser.currentText.ToString();
            string openingTagText = xmlParser.XmlOpenTags.Pop();
            if (!closingTagText.Equals(openingTagText)) {
                throw new InvalidDataException($"Incorrect closing tag <{closingTagText}> for opening tag <{openingTagText}> in XML");
            }
            xmlParser.currentText.Clear();
        }
    }
}