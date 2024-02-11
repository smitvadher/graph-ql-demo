# GraphQL Demo in .NET 8 with HotChocolate library
This repository demonstrates a GraphQL API built using .NET 8 and HotChocolate library, along with Entity Framework. The API supports basic operations for products and categories, as well as advanced features such as filtering, sorting, and projections.

## Getting Started
### Prerequisites
- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation
```bash
git clone https://github.com/smitvadher/graph-ql-demo.git
cd your-path\graph-ql-demo\src\Server\DemoApp
dotnet restore
```
### Running the Application
```bash
dotnet build
dotnet run
```
The GraphQL server will start, and you can access the GraphQL playground at https://localhost:7105/graphql or http://localhost:5105/graphql in your browser.

## Examples

### Categories
#### Get
```graphql'
query {
  categories(where: { products: { any: true } }, order: { name: ASC }) {
    name
    description
    imageUrl
    products(where: { isAvailable: { eq: true } }) {
      name
      description
      price
      images {
        url
      }
    }
  }
}
```
#### Create
```graphql
mutation {
  createCategory(
    input: {
      name: "Sample Category"
      description: "This is a sample category"
      imageUrl: "https://picsum.photos/640/480/?image=793"
    }
  )
}
```
#### Delete
```graphql
mutation {
  deleteCategory(categoryId: 11)
}
```

### Products
#### Get
```graphql
query {
  products(
    where: { price: { gte: 50 }, isAvailable: { eq: true } }
    order: { price: ASC }
  ) {
    id
    name
    description
    price
    category {
      name
    }
    images {
      url
    }
  }
}
```
#### Create
```graphql
mutation {
  createProduct(
    input: {
      name: "Sample Product"
      description: "This is a sample product."
      price: 19.99
      isAvailable: true
      categoryId: 1
    }
  )
}
```
#### Delete
```graphql
mutation {
  deleteProduct(productId: 101)
}
```
