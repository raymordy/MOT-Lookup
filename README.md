# Vehicle MOT Lookup Tool

A console application used to lookup MOT expiration dates by registration number.

The application will prompt for a registration number and then perform an MOT lookup displaying the Make, Model, Colour, MOT expiration date and last odometer reading.



## Installation and Launch

Clone the repository using this command:

```
git clone https://github.com/raymordy/MOT-Lookup.git
```

Alternatively you can use the download link (the green button above).
The solution targets .NET Core 3.1 and uses the packages below which will require a nugget restore.





### Packages Used

- Newtonsoft.Json
  - Provides deserialization for API response content
- Microsoft.Extensions.DependencyInjection
  - Dependency Injection container

### Illustrations

![Image of MOT Lookup application in action](https://raymordy.dev/wp-content/uploads/2020/07/MOT-Lookup.png)
