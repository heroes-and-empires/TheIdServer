# Claims providers

You can add your custom claims providers to the server. The [`ProfileService`](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Admin.Shared/Services/ProfileService.cs) uses claims providers defined in resources properties.

When a client asks for a resource (Identity or API), the [`ProfileService`](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Admin.Shared/Services/ProfileService.cs) looks for the **ClaimProviderType** key in its properties. If found, the dependency injection container is searched for this type's full name in the `IProvideClaims` collection and calls the method [`ProvideClaims`](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Shared/Absractions/IProvideClaims.cs) with the current context subject, client, caller, and properties.

If the provider is not found in the DI container and the property **ClaimProviderAssemblyPath** exists, it loads the assembly from this path and creates an instance of the provided type.

## Implement

Claims providers must implement the [`IProvideClaims`](/src/IdentityServer/Shared/Aguacongas.IdentityServer.Shared/Absractions/IProvideClaims.cs) interface.

**sample**

> The project [sample/Aguacongas.TheIdServer.CustomClaimsProvider](../sample/Aguacongas.TheIdServer.CustomClaimsProvider) contains an implementation sample.

```cs
public class MapClaimsProvider: IProvideClaims
{
    public Task<IEnumerable<Claim>> ProvideClaims(ClaimsPrincipal subject, Client client, string caller, Resource resource)
    {
        var defaultOutboundClaimMap = JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap;
        var claims = new List<Claim>(subject.Claims.Count());
        foreach (var claim in subject.Claims)
        {
            if (defaultOutboundClaimMap.TryGetValue(claim.Type, out string toClaimType))
            {
                claims.Add(new Claim(toClaimType, claim.Value, claim.ValueType, claim.Issuer));
            }
        }

        return Task.FromResult(claims as IEnumerable<Claim>);
    }
}
```

If your provider has dependencies, you can register it in the dependencies injection container by implementing the `ISetupClaimsProvider` interface.

**sample**

> The project [sample/Aguacongas.TheIdServer.CustomClaimsProvider](../sample/Aguacongas.TheIdServer.CustomClaimsProvider) contains an implementation sample.

```cs
public class ClaimsProvidersSetup : ISetupClaimsProvider
{
    public IServiceCollection SetupClaimsProvider(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("claims")
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration.GetValue<string>("ClaimsWebServiceUrl")));

        return services.AddTransient<IProvideClaims>(p => new WebServiceClaimsProvider(p.GetRequiredService<IHttpClientFactory>().CreateClient("claims")));
    }
}
```

> To avoid `TypeLoadException` copy *Aguacongas.IdentityServer.Duende.dll* or *Aguacongas.IdentityServer.IS4.dll* from your TheIdServer installation folder and reference it in your project:
> ```xml
> <Project Sdk="Microsoft.NET.Sdk">
>
>  <PropertyGroup>
>    <TargetFramework>net6.0</TargetFramework>
>    <Nullable>enable</Nullable>
>  </PropertyGroup>
>
>  <ItemGroup>
>    <Reference Include="Aguacongas.IdentityServer.Duende">
>      <HintPath>Aguacongas.IdentityServer.Duende.dll</HintPath>
>    </Reference>
>  </ItemGroup>
>
>  <ItemGroup>
>    <PackageReference Include="Duende.IdentityServer" Version="6.0.4" />
>  </ItemGroup>
> </Project>
> ```

## Configuration

To register your claims providers in the DI container, add the setup class declaration in the **ClaimsProviderOptions** configuration section.

```json
"ClaimsProviderOptions": [
    {
        "AssemblyPath": "{path to dll containing the setup class}",
        "TypeName": "{Full name of the setup class}"
    }
]
```

**sample**

```json
"ClaimsProviderOptions": [
    {
        "AssemblyPath": "Aguacongas.TheIdServer.CustomClaimsProviders.dll",
        "TypeName": "Aguacongas.TheIdServer.CustomClaimsProviders.ClaimsProvidersSetup"
    }
]
```

In a resource (Identity or API) your client asks for, add the property **ClaimProviderType** with full type name of a class implementing the [`IProvideClaims`](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Shared/Absractions/IProvideClaims.cs) interface.

If you do not register your providers in the DI container, add the path to the assembly containing this class in the property **ClaimProviderAssemblyPath**.

![claims-provider](assets/claims-provider-configuration.png)

## Public/private scenario

When the server acts as a proxy, the profile service is a [`ProxyProfileService`](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Admin.Shared/Services/ProxyProfilService.cs) instance. This class forwards each request to claims providers to the webservice endpoint */claimsprovider*.  
This way, you are not required to expose your claims providers to the Internet and don't have to open custom network firewall rules to access private resources (such as DB, private service, etc...) on the public side.

Read [Using the API](src/Aguacongas.TheIdServer/README.md#using-the-api) for information on how to configure a public/private cluster.

## Additional resources

 * [ProfileService class](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Admin.Shared/Services/ProfileService.cs)
 * [IProvideClaims interface](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Shared/Absractions/IProvideClaims.cs)
 * [ProxyProfilService class](../src/IdentityServer/Shared/Aguacongas.IdentityServer.Admin.Shared/Services/ProxyProfilService.cs)
 * [Using the API](/src/Aguacongas.TheIdServer.Duende/README.md#using-the-api)