Feature: Show Episode

Background: 
	Given Codecasts
	| title| published|permalink|
	| A	   | 3/1/2014 |episode-1|

@mytag
Scenario: Show Episode
	Given user U
	And that user U is logged in
	When the user request details for episode-1
	Then the presented title is A, published 3/1/2014
	And with option to purchase viewing license
	And with option to purchase <downloading> license
