# Blazor + WebAssembly + Grpc + Azure AD Authentication

## Info

### Project BlazorWasmGrpcWithAADAuth
Project created with commandline:
``` ps
dotnet new blazorwasm -au SingleOrg --api-client-id "821eb724-edb8-4dba-b425-3f953250c0ae" --app-id-uri "https://localhost:44375" --client-id "c0a70ecd-4c0d-417a-86cc-daba34d40538" --default-scope "API.Access" --domain "stefheyenrathgmail.onmicrosoft.com" -ho --tenant-id "020b0cf3-d6b2-464e-9b2d-45e124244428" -o BlazorWasmGrpcWithAADAuth
```

### Project BlazorWasmGrpcWithAADAuth2
Project created with commandline:
``` ps
dotnet new blazorwasm2 -au SingleOrg --api-client-id "821eb724-edb8-4dba-b425-3f953250c0ae" --app-id-uri "821eb724-edb8-4dba-b425-3f953250c0ae" --client-id "c0a70ecd-4c0d-417a-86cc-daba34d40538" --default-scope "API.Access" --domain "stefheyenrathgmail.onmicrosoft.com" -ho --tenant-id "020b0cf3-d6b2-464e-9b2d-45e124244428" -o Skills
```

#### Login
Login with `blazor@***.onmicrosoft.com`.

## References
- https://github.com/coding-flamingo/BlazorWasmWithAADAuth
- https://www.youtube.com/watch?v=6y2dSNX3xcc
- https://blog.sanderaernouts.com/grpc-aspnetcore-azure-ad-authentication