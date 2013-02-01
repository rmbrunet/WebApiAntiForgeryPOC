WebApi Antiforgery Token POC
==================================================

Proof of Concept for a full WebApi Antiforgery Token implementation. Instead of the hidden field normally used in MVC the token is sent to the client in a Http Header.

Two Http Filter Attributes are included: One that generates the pair of tokens (basically a substitution for the corresponding MVC Razor Helper) and another that performs the validation.

The client side is implemented using AngularJs but anything would work.    
