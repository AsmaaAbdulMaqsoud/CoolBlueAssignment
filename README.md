# CoolBlueAssignment
# Notes
- in the Api's layer appStart I am loading all products and product types to EFW inmemory Db.
- please make sure to run http://localhost:5002/ first before run the project.

1-BugFix:
    By looking to conditions sequence i found that speical type products check is not supposed to be inside the products basic insurance that calculated based on sales price.
 
2- Refactoring:
   for refactoring I applied clean archticture to have a maintainable and testable layers.
   I seperated the httprequests to a seperate service.
   I applied chain of responsibility design pattern to refactor the If statments for calculate insurance to be more readable and clean.
   I added serilog to keep the logs you can find it at bath "src/logs" # note that logs is deleted daily.
   I added MS test project for unit test.
   I added swagger and made it the default endpoint.
   
3- feature 1: 
   for this feature I found it will consume the enpoint of httpclient multiple times so for performance wise:
    - I added EFW inmemory Db as layer of caching and loaded all products and product types in appstart.
    - I implemented Repository to have a layer of abstration above the ORM layer 
	first I check the layer of caching if product dosn't exist in the caching layer then it consume the endpoint.
    
4- feature 2:
   for the additional insurance I added a bool field to check if order contains digital cameras and based on this field value 
   I am adding the order additional insurance.
   
5- feature 3:
   As the example mentioned in the slack channel I added the surcharge as a value not a rate and add it to the product insurance calculation
   if no surcharge added then the surcharge default value is zero.

   