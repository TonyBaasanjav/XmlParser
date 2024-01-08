# XmlParser

Simple XML checker

System logic:

0. Created stack to keep current open tags list when we encounter closing tag to check the most recent opening tags matches with it.
1. Created currentText string to keep either tag content or tag names.
2. Created XmlState to keep track of what state XML processor is currently in
  a. OpeningTagStarted -> indicates opening tag just started
    edge cases
    - we should be either in OpeningTagEnded or ClosingTagEnded in order to get this state
    - currentText should be empty since tag text should be only before ClosingTagStarted
  b. OpeningTagEnded -> indicates opening tag just ended, therefore we should put this tag value into the 'XML open tags stack'
    edge cases
    - we should be in OpeningTagStarted in order to get to this state
    - currentText should not be empty since tag name should be at least one char
  c. ClosingTagStarted -> indicates closing tag just started, we should handle the text inside tag
    - we should be in OpeningTagEnded or ClosingTagEnded state in order to get to this state
  d. ClosingTagEnded -> indicates closing tag just ended, we should check if tag names matches
    - we should be in ClosingTagStarted state only
3. Refactored ICharHandler out of XmlParser to make it more readable and more single responsibility using polymorphism
4. Trimmed initial xml input to skip any space character in end or start

Todo:

1. I felt like Handlers could be further divided into each XmlState's entry, but it could be controversial since the state is derived from same character (e.g: if we wrote OpeningTagStarted or ClosingTagStarted state handler it could derive from < character)

# Considered test cases: 

<People><Design>correct</Design><Code>hello world</Code></People>             -> correct
<People>dsads<Design>correct</Design><Code>hello world</Code></People>        -> Unexpected string dsads
<><Design>correct</Design><Code>hello world</Code></People>                   -> Empty tag name at 1
<People><Design></Design><Code>hello world</Code></People>                    -> correct
<People><Design>correct</Design><Code>hello world</</Code></People>           -> Unexpected closing tag at 51
 <People><Design>correct</Design><Code>hello world</Code></People>            -> correct (trim starting and ending spaces)

