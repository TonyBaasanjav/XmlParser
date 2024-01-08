
namespace ConsoleApp1 {

    public class CharHandler : ICharHandler
    {
        private XmlParser xmlParser;

        public CharHandler(XmlParser xmlParser) {
            this.xmlParser = xmlParser;
        }

        public int handle(int currentIndex)
        {
            if(xmlParser.currentState == XmlState.OpeningTagStarted || xmlParser.currentState == XmlState.ClosingTagStarted) {
                handleTagName(xmlParser.charArray[currentIndex]);
            } else if (xmlParser.currentState == XmlState.OpeningTagEnded) {
                handleTagValue(xmlParser.charArray[currentIndex]);
            } else {
                throw new InvalidDataException($"Unexpected character '{xmlParser.charArray[currentIndex]}' in XML at {currentIndex}");
            }
            return currentIndex;
        }

        public void handleTagName(char ch) {
            xmlParser.currentText.Append(ch);
        }

        public void handleTagValue(char ch) {
            xmlParser.currentText.Append(ch);
        }
    }
}