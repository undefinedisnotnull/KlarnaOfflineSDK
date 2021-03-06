# Klarna Offline SDK

**Library that integrates and utilizes the Klarna offline API**

The full API specs are avaliable at apiary -> http://docs.klarnaoffline.apiary.io/

## Installing the SDK   

```c#
Install-Package Mnording.Klarna.Offline
```

## Setting up the cart and config

Firstly you create a config of the current culture, your currency, country, shared secret and store ID.
```c#
MerchantConfig config = MerchantConfig(CultureInfo.CurrentCulture, Currency.SEK, Country.SE, "YOURSharedSECRET", "MERCHANT_ID");
```
To recieve your merchant ID and shared secret for your integration you will need to reach out to Klarna.  

You then create a cart, and populate it with items
```c#
Cart cart = new Cart();  
cart.addProduct(new CartRow("Prod1234", "New Shoes for you", 1, 10000, 25));  
cart.addProduct(new CartRow("Prod1233", "New purple for you", 1, 10000, 25));
```
You are also able to define discounts for the cart.
```c# 
cart.addDiscount(new CartRow("discount-1", "Summer sales", 1, -1000,25));
```
*Note:* Prices are entered with amount of cents. Meaning a product that costs 10 SEK, you must enter 1000.

##  Creating the order
First you send in the cart and config and an optional push url    

**Use polling method**  
By only starting the order, you will receive a status url hosted by klarna that will communicate the order details once the customer has completed the purchase.

 ```c#
OfflineOrder offlineOrder = new OfflineOrder(cart, config, "terminal", phone, "Merchant_OrderReference");
 ```
**Use push url method** 
If you define a status url, then order-data will be pushed to that url when customer has completed the purchase.

```c#
OfflineOrder offlineOrder = new OfflineOrder(cart, config, "terminal", phone, "1", new Uri("https://URLThatShouldReceiveOrderInformation.com"));
```

**Create the order**  
The create call will create the actual KCO session and send out the SMS to the consumer
```c#
offlineOrder.Create();
```

If you did not define your own status URL, Klarna will create one for you that you will use for polling the result of the transaction

**Cancel ongoing order** 
```c#
offlineOrder.Cancel();
```
Note: Order must have been created before you can cancel it.


## Reading the customer details
**Using polling method:**  
If you wanted Klarna to create a status url for you, you can use the url that is created on the order, to poll for information.
```c#
string url = offlineOrder.GetStatusUrl();
```
This url will timeout every 60 seconds and you will need to re-trigger it to check as long as the customer has not completed the purchase
```c#
OrderDetails details = offlineOrder.pollData(url);
```

**Using status url method**  
If you defined your own statusurl, Klarna will post data to that url. To read all order-data you can use the JsonConverter.
```c#
OrderDetails details =JsonConverter.GetOrderFromString(jsonString);
```
