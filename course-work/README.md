{
  "openapi": "3.0.1",
  "info": {
    "title": "E-Commerce API",
    "version": "v1"
  },
  "paths": {
    "/api/Auth/register": {
      "post": {
        "tags": [
          "Auth"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Auth/login": {
      "post": {
        "tags": [
          "Auth"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/LoginDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Categories": {
      "get": {
        "tags": [
          "Categories"
        ],
        "parameters": [
          {
            "name": "Name",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "Page",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "PageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "Search",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "SortBy",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "Descending",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/CategoryResponseDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/CategoryResponseDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/CategoryResponseDto"
                  }
                }
              }
            }
          }
        }
      },
      "post": {
        "tags": [
          "Categories"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CategoryRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CategoryRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CategoryRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Categories/{id}": {
      "put": {
        "tags": [
          "Categories"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CategoryRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CategoryRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CategoryRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "delete": {
        "tags": [
          "Categories"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Orders": {
      "get": {
        "tags": [
          "Orders"
        ],
        "parameters": [
          {
            "name": "Status",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "IsPaid",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          },
          {
            "name": "FromDate",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "date-time"
            }
          },
          {
            "name": "ToDate",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "date-time"
            }
          },
          {
            "name": "Page",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "PageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "Search",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "SortBy",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "Descending",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/OrderResponseDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/OrderResponseDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/OrderResponseDto"
                  }
                }
              }
            }
          }
        }
      },
      "post": {
        "tags": [
          "Orders"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/OrderRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/OrderRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/OrderRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Orders/{id}": {
      "get": {
        "tags": [
          "Orders"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "put": {
        "tags": [
          "Orders"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/OrderUpdateDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/OrderUpdateDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/OrderUpdateDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "delete": {
        "tags": [
          "Orders"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Orders/{orderId}/total-price": {
      "get": {
        "tags": [
          "Orders"
        ],
        "parameters": [
          {
            "name": "orderId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Orders/{id}/status": {
      "patch": {
        "tags": [
          "Orders"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateOrderStatusDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateOrderStatusDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateOrderStatusDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Orders/{orderId}/pay": {
      "patch": {
        "tags": [
          "Orders"
        ],
        "parameters": [
          {
            "name": "orderId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdatePaymentDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdatePaymentDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/UpdatePaymentDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Products": {
      "get": {
        "tags": [
          "Products"
        ],
        "parameters": [
          {
            "name": "MinPrice",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "MaxPrice",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "CategoryId",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "Page",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "PageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "Search",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "SortBy",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "Descending",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ProductResponseDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ProductResponseDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ProductResponseDto"
                  }
                }
              }
            }
          }
        }
      },
      "post": {
        "tags": [
          "Products"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/ProductRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/ProductRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/ProductRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Products/{id}": {
      "get": {
        "tags": [
          "Products"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/ProductResponseDto"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProductResponseDto"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProductResponseDto"
                }
              }
            }
          }
        }
      },
      "put": {
        "tags": [
          "Products"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/ProductRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/ProductRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/ProductRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "delete": {
        "tags": [
          "Products"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Users": {
      "get": {
        "tags": [
          "Users"
        ],
        "parameters": [
          {
            "name": "Role",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "Email",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "Page",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "PageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "Search",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "SortBy",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "Descending",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/UserResponseDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/UserResponseDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/UserResponseDto"
                  }
                }
              }
            }
          }
        }
      },
      "post": {
        "tags": [
          "Users"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/UserRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/UserRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/UserRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "delete": {
        "tags": [
          "Users"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Users/{id}": {
      "get": {
        "tags": [
          "Users"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/UserResponseDto"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/UserResponseDto"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/UserResponseDto"
                }
              }
            }
          }
        }
      },
      "put": {
        "tags": [
          "Users"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/UserRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/UserRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/UserRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "CategoryRequestDto": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "CategoryResponseDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "products": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/ProductCategoryResponseDto"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "LoginDto": {
        "type": "object",
        "properties": {
          "email": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "OrderItemRequestDto": {
        "type": "object",
        "properties": {
          "productId": {
            "type": "integer",
            "format": "int32"
          },
          "quantity": {
            "type": "integer",
            "format": "int32"
          }
        },
        "additionalProperties": false
      },
      "OrderItemResponseDto": {
        "type": "object",
        "properties": {
          "productId": {
            "type": "integer",
            "format": "int32"
          },
          "quantity": {
            "type": "integer",
            "format": "int32"
          },
          "price": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "OrderRequestDto": {
        "type": "object",
        "properties": {
          "userId": {
            "type": "integer",
            "format": "int32"
          },
          "address": {
            "type": "string",
            "nullable": true
          },
          "orderDate": {
            "type": "string",
            "format": "date-time"
          },
          "items": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/OrderItemRequestDto"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "OrderResponseDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "orderDate": {
            "type": "string",
            "format": "date-time"
          },
          "totalPrice": {
            "type": "number",
            "format": "double"
          },
          "address": {
            "type": "string",
            "nullable": true
          },
          "isPaid": {
            "type": "boolean"
          },
          "status": {
            "$ref": "#/components/schemas/OrderStatus"
          },
          "items": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/OrderItemResponseDto"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "OrderStatus": {
        "enum": [
          "Pending",
          "Processing",
          "Shipped",
          "Delivered",
          "Cancelled"
        ],
        "type": "string"
      },
      "OrderUpdateDto": {
        "type": "object",
        "properties": {
          "orderDate": {
            "type": "string",
            "format": "date-time"
          },
          "totalPrice": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "ProductCategoryResponseDto": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "ProductRequestDto": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "price": {
            "type": "number",
            "format": "double"
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "categoryId": {
            "type": "integer",
            "format": "int32"
          }
        },
        "additionalProperties": false
      },
      "ProductResponseDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "name": {
            "type": "string",
            "nullable": true
          },
          "price": {
            "type": "number",
            "format": "double"
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "category": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "RegisterDto": {
        "type": "object",
        "properties": {
          "username": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "UpdateOrderStatusDto": {
        "type": "object",
        "properties": {
          "status": {
            "$ref": "#/components/schemas/OrderStatus"
          }
        },
        "additionalProperties": false
      },
      "UpdatePaymentDto": {
        "type": "object",
        "properties": {
          "amount": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "UserRequestDto": {
        "type": "object",
        "properties": {
          "username": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          },
          "passwordHash": {
            "type": "string",
            "nullable": true
          },
          "role": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "UserResponseDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "username": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          },
          "role": {
            "type": "string",
            "nullable": true
          },
          "createdAt": {
            "type": "string",
            "format": "date-time"
          },
          "orders": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/OrderResponseDto"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      }
    },
    "securitySchemes": {
      "Bearer": {
        "type": "http",
        "description": "Enter JWT Token",
        "scheme": "bearer",
        "bearerFormat": "JWT"
      }
    }
  },
  "security": [
    {
      "Bearer": [ ]
    }
  ]
}
