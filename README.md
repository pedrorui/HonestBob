HonestBob
=========

Honest Bob's Demo Web Application

The Honest Bob’s POC is a Visual Studio 2013 Solution, using a SQL Server 2012 database. It implements a product catalogue and a shopping basket, using ASP.Net MVC 5.1 and ASP.Net Web API 2.1
The product catalogue was implemented using a multiple table inheritance pattern, although in a real life situation the entity attribute value pattern would probably more suitable, with the search delegated to solr or similar.
Some of the features include the usage of AutoFac as IOC to inject dependencies on the controllers, caching and a multi-tier design, with the goal to provide a decoupled and extensible design.

