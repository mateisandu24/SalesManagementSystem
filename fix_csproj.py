import sys

with open('SalesManagementSystem.csproj', 'r', encoding='utf-8') as f:
    content = f.read()

admin_forms = ['AdminCommandsForm', 'AdminOrdersForm', 'EditProductForm']
client_forms = ['CartForm', 'ClientOrdersForm', 'ProductDetailsForm', 'Form1']
inout_forms = ['LoginForm', 'RegisterForm', 'InputForm']

for f in admin_forms:
    content = content.replace(f'Forms\\{f}', f'Forms\\Admin\\{f}')

for f in client_forms:
    content = content.replace(f'Forms\\{f}', f'Forms\\Client\\{f}')

for f in inout_forms:
    content = content.replace(f'Forms\\{f}', f'Forms\\InOut\\{f}')

with open('SalesManagementSystem.csproj', 'w', encoding='utf-8') as f:
    f.write(content)
