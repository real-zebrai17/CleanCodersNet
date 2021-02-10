Feature: Episode2: Present Codecasts

@mytag
Scenario: Present Downloadable Codecasts
	Given CodecastsList
		| title | published |
		| A     | 3/1/2014  |
		| B     | 3/2/2014  |
		| C     | 2/18/2014 |
	And user <U>
	And that user <U> is logged in
	And with liscense for <U> able to view <A>
	Then then following codecats will be presented for <U>
	And Ordered query:of Codecasts 
	| title | picture | description | viewable | downloadable |
	| C     | C       | C           | -        | -            |
	| A     | A       | A           | -        | +            |
	| B     | B       | B           | -        | -            |

Scenario: Present Viewable Codecasts
	Given CodecastsList
		| title | published |
		| A     | 3/1/2014  |
		| B     | 3/2/2014  |
		| C     | 2/18/2014 |
	And user <U>
	And that user <U> is logged in
	And with liscense for <U> able to view <A>
	Then then following codecats will be presented for <U>
	And Ordered query:of Codecasts 
	| title | picture | description | viewable | downloadable |
	| C     | C       | C           | -        | -            |
	| A     | A       | A           | +        | -            |
	| B     | B       | B           | -        | -            |


