﻿Feature: Episode1: Present Codecasts

@mytag
Scenario: Present No Codecasts
	Given no codecasts
	Given user <U>
	And that user <U> is logged in
	Then then following codecats will be presented for <U>
	Then there will be no codecasts presented
