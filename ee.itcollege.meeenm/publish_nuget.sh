dotnet  nuget push \
    --source https://api.nuget.org/v3/index.json \
    --api-key oy2pg56zhjwo6lu3wygajoed3vm3tgwe7mtrmlc3iwhnke \
    Contracts.Domain/bin/Release/com.mubbly.gifterapp.Contracts.Domain.1.0.0.nupkg \
    --skip-duplicate

dotnet nuget push \
    --source https://api.nuget.org/v3/index.json \
    --api-key oy2pg56zhjwo6lu3wygajoed3vm3tgwe7mtrmlc3iwhnke \
    Domain.Base/bin/Release/com.mubbly.gifterapp.Domain.Base.1.0.0.nupkg \
    --skip-duplicate
    
dotnet  nuget push \
    --source https://api.nuget.org/v3/index.json \
    --api-key oy2pg56zhjwo6lu3wygajoed3vm3tgwe7mtrmlc3iwhnke \
    Contracts.DAL.Base/bin/Release/com.mubbly.gifterapp.Contracts.DAL.Base.1.0.0.nupkg \
    --skip-duplicate
    
dotnet  nuget push \
    --source https://api.nuget.org/v3/index.json \
    --api-key oy2pg56zhjwo6lu3wygajoed3vm3tgwe7mtrmlc3iwhnke \
    DAL.Base/bin/Release/com.mubbly.gifterapp.DAL.Base.1.0.0.nupkg \
    --skip-duplicate
    
dotnet  nuget push \
    --source https://api.nuget.org/v3/index.json \
    --api-key oy2pg56zhjwo6lu3wygajoed3vm3tgwe7mtrmlc3iwhnke \
    DAL.Base.EF/bin/Release/com.mubbly.gifterapp.DAL.Base.EF.1.0.0.nupkg \
    --skip-duplicate
    
dotnet  nuget push \
    --source https://api.nuget.org/v3/index.json \
    --api-key oy2pg56zhjwo6lu3wygajoed3vm3tgwe7mtrmlc3iwhnke \
    Contracts.BLL.Base/bin/Release/com.mubbly.gifterapp.Contracts.BLL.Base.1.0.0.nupkg \
    --skip-duplicate
 
dotnet  nuget push \
    --source https://api.nuget.org/v3/index.json \
    --api-key oy2pg56zhjwo6lu3wygajoed3vm3tgwe7mtrmlc3iwhnke \
    BLL.Base/bin/Release/com.mubbly.gifterapp.BLL.Base.1.0.0.nupkg \
    --skip-duplicate
