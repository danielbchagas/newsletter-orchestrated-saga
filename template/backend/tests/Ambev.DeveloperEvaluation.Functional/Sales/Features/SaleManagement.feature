Feature: SaleManagement

A short summary of the feature

#@tag1
#Scenario: [scenario name]
#	Given [context]
#	When [action]
#	Then [outcome]

@AddItemToSale
Scenario: Add item to sale
    Given a new sale
	When I add an item should be added to the sale
    Then the sale should contain the item

@RemoveItemFromSale
Scenario: Remove item from sale
	Given a sale with items
	When I remove an item from the sale
	Then the sale should not contain the item

@CalculateTotal
Scenario: Calculate total
	Given a sale with items
	When I calculate the total
	Then the total should be the sum of the items

@CalculateTotalWithDiscount
Scenario: Calculate total with discount
	Given a sale with items
	When I calculate the total with discount
	Then the total should be the sum of the items minus the discount

