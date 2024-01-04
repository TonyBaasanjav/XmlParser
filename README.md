# XmlParser

Simple XML checker

System logic:

0. Created stack to keep current open tags list when we encounter closing tag to check the most recent opening tags matches with it.
1. Created currentText string to keep either tag content or tag names.
2. Created XmlState to keep track of what state XML processor is currently in
  a. OpeningTagStarted -> indicates opening tag just started
    edge cases
    - !we should be either in OpeningTagEnded or ClosingTagEnded in order to get this state
    - !currentText should be empty since tag text should be only before ClosingTagStarted
  b. OpeningTagEnded -> indicates opening tag just ended, therefore we should put this tag value into the 'XML open tags stack'
    edge cases
    - we should be in OpeningTagStarted in order to get to this state
  c. ClosingTagStarted -> indicated closing tag just started, therefore we should check if there is currentText which means 
    - !we should be in OpeningTagEnded state in order to get to this state
3. 

# Todo: 