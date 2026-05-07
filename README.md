# Sales Management System 🛒

A robust, full-featured desktop application built with **C# Windows Forms (WinForms)** and **.NET Framework 4.7.2**. The application functions as an eCommerce/Sales Management platform, supporting product browsing, cart functionalities, immediate purchasing, user authentication, and comprehensive administrative tools.

## 🚀 Key Features

### 👤 User Authentication
* **Login & Registration:** Complete secure authentication workflow.
* **UI/UX Polishes:** Dynamic "Show Password" (Arată parola) toggles separated cleanly into Designer files for scalable UI.

### 🛍️ Product Catalog (Dashboard)
* **Dynamic Grid View:** Products are listed with attributes like Name, Price, and Stock.
* **Async Image Loading:** Product images are seamlessly fetched over the network from Wix storage formats without freezing the UI.
* **Visual Polish:** A `loading.gif` plays smoothly on image cells while the request processes, switching out natively to the product bitmap upon completion. Faulty URLs fallback to an invisible empty cell rather than crashing or showing red 'X' errors.
* **Responsive Layout:** Search bars, filtering dropdowns, and grids utilize proper WinForm Anchoring, keeping the interface fluid regardless of window maximization or scaling (DPI).

### 🏷️ Product Details & Checkout
* **Rich Descriptions:** Selecting a product opens a detailed breakdown with a scalable image and rich-text description.
* **Instant Checkout:** "Cumpără Acum" (Buy Now) functionality using a `NumericUpDown` selector configured logically (caps automatically at available stock).
* **Database Transactions:** Direct buying calls a transactional database method (`PlaceOrderDirect`), ensuring that creating the order record and decrementing the product stock happens atomically.

### ⚙️ Admin Dashboard
* **CSV Imports:** Admins can quickly bulk-import items into the system.
* **Order Tracking:** Monitor client purchases and order histories.
* **Clean Organization:** Dashboard buttons are neatly sized, styled with flat modern properties, and utilize clear emojis (📂, 📋) for immediate visual feedback.

## 🛠️ Architecture & Technology Stack

* **Framework:** .NET Framework 4.7.2
* **Language:** C# 7.3
* **Database Access:** **Dapper** (Micro-ORM) alongside `System.Data.SqlClient`. This ensures fast, parameterized SQL queries safe from injections.
* **Pattern:** The application strictly adheres to the **Repository Pattern**. Database logic is abstracted away into dedicated classes (`UserRepository`, `ProductRepository`, `OrderRepository`), keeping the WinForms code-behinds focused solely on presentation.
* **Theme Management:** A centralized `ThemeManager` controls UI styling, enabling consistent application-wide themes (dark accents, flat borders, matching font families).
* **Designer Segregation:** A strict Clean Code rule was enforced: No dynamic instantiation of UI elements (`new Button()`, `new Label()`, etc.) happens inside runtime logic `.cs` files. All controls are strictly declared and auto-scaled within the `*.Designer.cs` partial classes.

## 🗄️ Database Schema & Transactions
The SQL Server backend supports relational data across Users, Products, Shopping Carts, Transactions, and Order items. Key transactional integrity is implemented in the `OrderRepository`. When an order is placed:
1. An SQL Transaction starts.
2. The order is inserted into the Orders table.
3. Order items are recorded.
4. Product Stock is decremented (via `UPDATE Products SET Stock = Stock - 1 WHERE Id = @Id AND Stock > 0`).
5. Transaction commits (or rolls back safely if an exception occurs).

## 🎨 UI/UX Highlights
* **Flat Design:** WinForms native 3D borders are flattened for a more modern aesthetic.
* **Responsiveness:** AutoScale mode ensures form layouts don't break when switching monitors or changing OS font scales.
* **Memory Management:** Forms and image Streams are correctly disposed to prevent memory leaks during intense asynchronous grid loading operations.
