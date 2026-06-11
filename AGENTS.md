# Bekobod 24 – Summary

## Goal
Create a web bot (Blazor WebAssembly) for client and courier UI with Flutter-like design, keeping admin panel in Flutter.

## Constraints & Preferences
- Backend: .NET 8, EF Core + Npgsql, JWT auth, SignalR hub at `/hubs/orders`
- Client + courier UI moves from Flutter to a web app (Blazor WASM) accessible via mobile browser
- Admin panel stays in Flutter with Material 3 design (`#1565C0` primary blue)
- Web bot must have a similar look and feel to the original Flutter client catalog and courier screens (cards, grids, images, ratings)
- `/api/files/upload` for image upload, `picsum.photos/seed/{id}/400/400` as placeholder fallback
- `flutter analyze` must have 0 errors

## Progress
### Done
- **Backend – Category image**: `Category` entity has `ImageId` + `Image` navigation; `CategoryConfiguration` updated with `HasOne(c => c.Image)`; migration `AddCategoryImageId` created and applied
- **Backend – DTOs**: All product/category/order/courier DTOs include `ImageId` / `ImageUrl` with placeholder fallback
- **Flutter – Theme & Login**: Material 3 theme with `#1565C0` primary; login screen with blue button
- **Flutter – Admin screens**: Dashboard, products CRUD, categories CRUD — all with image picker/upload, thumbnails, FAB, pull-to-refresh
- **Flutter – Bugfix (overflow)**: Categories screen list item `Column` with `IconButton`s — compacted to `36×36` constraints + `padding: EdgeInsets.zero` to fit inside `SizedBox(height: 88)`
- **Flutter – Bugfix (hit-test)**: `_imagePlaceholder()` given explicit `width: double.infinity, height: 160` so `GestureDetector` always has a sized child; removed `SizedBox` wrappers that caused `mouse_tracker.dart !_debugDuringDeviceUpdate` assertion on web
- **TelegramBot (merged into WebAPI)**: Bot functionality moved from separate project into `WebAPI/Bot/` as a `BackgroundService` — no separate process needed:
  - `BotService.cs` – polling via `Telegram.Bot` 19.0.0, auto-starts with WebAPI
  - `CommandHandler.cs` – `/start` (WebApp catalog), `/courier` (WebApp courier)
  - Config in `appsettings.json` under `Bot:BotToken` and `Bot:WebAppUrl`
  - Runs inside the same `WebAPI` process, shares `AppDbContext` and DI
- **WebBot (Blazor WASM)**: Created `WebBot/` — Blazor WebAssembly project in the solution; mobile-first layout with `#1565C0` theme, bottom nav bar, Flutter-like card/grid CSS
  - **Models**: `StoreType`, `Store`, `Category`, `Product`, `Order`, `OrderItem`, `CartItem`, `CourierProfile`, `AuthResponse`, `CreateOrderRequest`
  - **Services**: `ApiService` (all API endpoints), `AuthState` (JWT + role), `CartState` (in-memory cart)
  - **Client pages**: Home (store types grid), Stores (with images, rating), Products (category chips + add-to-cart), Cart (qty controls + checkout), Orders (order history), Login/Register
  - **Courier pages**: Login, Dashboard (profile, available orders, active orders with accept/deliver), History
  - **CSS**: Flutter-inspired `#1565C0` primary, cards, grids, order status badges, mobile-first responsive
  - **Build**: `dotnet build` → 0 errors, 0 warnings
- **Backend CORS**: Already configured with `SetIsOriginAllowed(_ => true)`, no changes needed
- **WebBot – Telegram WebApp integration**: Added `Services/TelegramService.cs` (JS interop for `window.Telegram.WebApp` — ready, expand, back button, sendData, getUser), `MainLayout` hides bottom nav in Telegram context, `wwwroot/index.html` has Telegram WebApp init script
- **`flutter analyze`**: 0 errors, 0 warnings (15 info)
- **Single-origin architecture**: WebAPI now serves both API controllers AND Blazor WASM static files from `WebBot/publish/wwwroot` via `PhysicalFileProvider`. One tunnel to port 5000 handles everything.
- **ApiService uses relative URLs**: `HttpClient.BaseAddress` = `{page_origin}/api/` — no absolute/hardcoded URLs.
- **Fixed missing usings**: `Program.cs` now has `Services.Interfaces`, `Services.Implementations`, `Services.Settings`, `Telegram.Bot`.

### In Progress
- (none)

### Blocked
- (none)

## Key Decisions
- **Telegram WebApp bot**: Bot sends inline buttons with `WebAppInfo` URL → opens Blazor WASM inside Telegram's built-in WebView (not inline keyboards)
- Telegram bot is the launcher; Blazor WASM is the full UI — runs in Telegram WebView or standalone browser
- Blazor WASM uses `Telegram.WebApp` JS API: `ready()`, `expand()`, `BackButton`, `sendData()`, `initDataUnsafe.user`
- Admin panel remains in Flutter; only client catalog and courier functionality moves to the web bot
- Image upload flow unchanged: pick → upload to `/api/files/upload` → attach `imageId` to entity

## Next Steps
1. Publish WebBot: `dotnet publish WebBot/WebBot.csproj -o WebBot/publish -p:PublishTrimmed=false`
2. Start backend + bot: `dotnet run --project WebAPI` (bot starts automatically, also serves Blazor WASM)
3. Expose via one tunnel: `cloudflared tunnel --url http://localhost:5000`
4. Copy tunnel HTTPS URL to `WebAPI/appsettings.json` → `Bot:WebAppUrl`
5. Restart WebAPI so bot uses the new URL
6. Test in Telegram: `/start` → tap "Open Catalog" → browse → order
7. Test courier: `/courier` → login → accept → deliver

## Critical Context
- Backend URL: `http://localhost:5000/api`
- WebBot served from: `WebAPI` (static files from `WebBot/publish/wwwroot`)
- File upload: `POST /api/files/upload` (multipart) → returns `{id, url}`
- Image download: `GET /api/files/download/{id}`
- Placeholder: `https://picsum.photos/seed/{entityId}/400/400`
- WebAPI controllers: `api/store-types`, `api/stores`, `api/categories`, `api/products`, `api/orders`, `api/courier`, `api/auth` — all return JSON
- CORS: `SetIsOriginAllowed(_ => true)` in `WebAPI/Program.cs`
- `flutter analyze` → 0 errors, 0 warnings, 15 info
- `dotnet build` (solution) → 0 errors, 0 warnings

## Relevant Files
- **Backend**:
  - `Core/Entities/` – `Category`, `Product`, `Order`, `Store`, `StoreType`, `Courier`, `FileModel`, `User`
  - `Infrastructure/AppDbContext.cs` – EF Core context
  - `Services/DTOs/` – all API response/request DTOs
  - `WebAPI/Controllers/` – `AuthController`, `StoreTypesController`, `StoresController`, `CategoriesController`, `ProductsController`, `OrdersController`, `CourierController`, `FilesController`
- **Flutter (admin only)**:
  - `lib/screens/admin/admin_dashboard_screen.dart`
  - `lib/screens/admin/admin_products_screen.dart`
  - `lib/screens/admin/admin_categories_screen.dart`
  - `lib/core/theme.dart`
  - `lib/screens/auth/login_screen.dart`
- **WebBot (Blazor WASM)**:
  - `WebBot.csproj` – .NET 8 Blazor WebAssembly
  - `Program.cs` – DI for AuthState, CartState, ApiService, TelegramService
  - `Services/ApiService.cs` – all REST calls to WebAPI
  - `Services/AuthState.cs` – JWT token + role management
  - `Services/CartState.cs` – in-memory cart with quantity controls
  - `Services/TelegramService.cs` – JS interop for Telegram.WebApp API
  - `Models/` – `StoreType`, `Store`, `Category`, `Product`, `Order`, `CartItem`, `CourierProfile`, `AuthResponse`, `CreateOrderRequest`
  - `Pages/Home.razor` – store types grid (2-column)
  - `Pages/Client/Stores.razor` – store list with images, rating, address
  - `Pages/Client/Products.razor` – category chips, product cards, add-to-cart
  - `Pages/Client/Cart.razor` – cart with qty controls, checkout form
  - `Pages/Client/Orders.razor` – order history for auth clients
  - `Pages/Client/Login.razor` – login/register toggle
  - `Pages/Courier/Login.razor` – courier login
  - `Pages/Courier/Dashboard.razor` – profile, available/active orders, accept/deliver
  - `Pages/Courier/History.razor` – delivery history
  - `Layout/MainLayout.razor` – app bar + sticky bottom nav (hidden in Telegram)
  - `wwwroot/index.html` – Telegram WebApp init script
  - `wwwroot/css/app.css` – Flutter-inspired theme (`#1565C0`)
- **WebAPI (Backend + Bot)**:
  - `Program.cs` – DI with `BotService` (hosted) + `CommandHandler` (scoped)
  - `Bot/BotService.cs` – BackgroundService, polling via Telegram.Bot 19.0.0
  - `Bot/CommandHandler.cs` – `/start` (WebApp catalog), `/courier` (WebApp courier)
  - `appsettings.json` – `Bot:BotToken`, `Bot:WebAppUrl`, DB connection
