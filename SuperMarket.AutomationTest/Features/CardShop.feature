@CardShopFeature
Feature: CardShop
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@AddProduct
Scenario: 01 Add product to cardShop
	Given Add product to cardShop
	Then Check result cardshop should be two

@DeleteFirstProduct
Scenario:  02 Delete first product to cardshop
	Given Go to the card shop page
	And Delete first product to cardshop
	Then the result table should be is 2 sub one

@DeleteLastProduct
Scenario: 03 Delete last product to cardshop
	Given Go to the card shop page
	And Delete last product to cardshop
	Then the result table should be an empty
	
@CheckCardShopWithProductAdded
Scenario: 04 Check cardshop with product added
	Given Go to Groceries,  Household and Personal Care page add products
	And Go to the card shop page
	Then Check list product correct with product added

@UpdateQuanlityAndVerifyPrice
Scenario: 05 Update quanlity and verify price
	Given Go to the card shop page
	And Update quanlity is "2" with first product on list
	Then Total price must be match with quanlity*price

@UpdateQuanlityWithNegativeNumber
Scenario: 06 Update quanlity with negative number
	Given Go to the card shop page
	And Update quanlity with negative number is "-3" on last item in list
	Then Update negative number is -3

@UpdateQuanlityWithPositveNumber
Scenario: 07 Update quanlity with positive number
	Given Go to the card shop page
	And Update quanlity with positive number is "3" on first item in list
	Then Update positive number is 3

@UpdateQuanlityWithNotNumber
Scenario: 08 Update quanlity with not number
	Given  Go to the card shop page
	And Update quanlity with not number is "abc" and value before is 2 on fist item in list
	Then Value after updated must be before value is 2

@ClickItemGoToDetailPage
Scenario: 09 Click item go to detail page
	Given Click any item on the list
	Then Verify the page is the details page

@HiddenCheckOutWhenHasItemNegativeQuanlity
Scenario: 10 Hidden check out button when data incorrect
	Given Go to the card shop page
	Then Check button checkout hidden when has item negative quanlity
	
@CheckTotalPriceMatchTotalPriceInList
Scenario: 11 Check total price match with total price in list
	Given Update data correct show check out button
	Then Check total price match with total price in list
	
@CheckTotalPriceMatchWithOrderTotal
Scenario: 12 Check total price match with order total
	Given Check out item in card shop
	And Login check out item
	Then Check total price match with order total

@CheckOutItemInCardShop
Scenario: 13 Check out item in card shop
	Given Click paynow
	Then Checkout success