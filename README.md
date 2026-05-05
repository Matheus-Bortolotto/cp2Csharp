# CP2 — Projeto Banco Digital

## 1. Identificação

| Nome | RM |
|------|----|
| Matheus Bortolotto | RM555189 |
| Matheus Ricciotti | RM556930 |

## 2. Produto Bancário Escolhido

**Empréstimo** com cálculo de score de crédito.

O empréstimo foi escolhido por permitir implementar uma regra de negócio real: o sistema calcula automaticamente um score de crédito do cliente com base na sua idade e no valor solicitado. Se o score atingir 500 ou mais (escala de 0 a 1000), a contratação é aprovada automaticamente; caso contrário, é rejeitada.

## 3. Modelagem de Filas

Não aplicável — o professor confirmou durante a aula que mensageria (RabbitMQ) não seria necessária para esta entrega. O processamento da contratação é feito de forma síncrona, com o score calculado no momento da requisição.

## 4. Diagrama de Classes

> Inserir imagem do diagrama aqui (docs/diagrama-classes.png)
<img width="641" height="581" alt="diagrama-classes drawio" src="https://github.com/user-attachments/assets/e85032e5-a47f-4ee9-8015-9ec4f723c2ce" />


## 5. Como Rodar Localmente

### Pré-requisitos
- Visual Studio 2022
- .NET 8.0 SDK
- Acesso à rede FIAP (Oracle)

### Configurar connection string

No arquivo `appsettings.json`, substituir com suas credenciais:

```json
"OracleConnection": "User Id=SEU_RM;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL;"
```

### Criar as tabelas

No Package Manager Console:

```powershell
Add-Migration CriarTabelas
Update-Database
```

### Rodar o projeto

```powershell
dotnet run
```

Ou pressionar **F5** no Visual Studio 2022.

## 6. Endpoints Disponíveis

### POST /api/Agencias
Cadastra uma agência.

**Request:**
```json
{
  "nmEndereco": "Rua das Flores, 100",
  "cep": "01310-100"
}
```
**Response 201:**
```json
{
  "idAgencia": 1,
  "nmEndereco": "Rua das Flores, 100",
  "cep": "01310-100"
}
```

---

### GET /api/Agencias/{id}
Busca agência por ID.

**Response 200:**
```json
{
  "idAgencia": 1,
  "nmEndereco": "Rua das Flores, 100",
  "cep": "01310-100"
}
```

---

### POST /api/Clientes/pf
Cadastra pessoa física.

**Request:**
```json
{
  "nmCliente": "João Silva",
  "email": "joao@email.com",
  "cpf": "123.456.789-00",
  "dataNascimento": "1990-01-15",
  "idAgencia": 1
}
```
**Response 201:**
```json
{
  "cpf": "123.456.789-00",
  "dataNascimento": "1990-01-15T00:00:00",
  "idCliente": 1,
  "nmCliente": "João Silva",
  "email": "joao@email.com",
  "tipoCliente": "PF",
  "idAgencia": 1
}
```

---

### POST /api/Clientes/pj
Cadastra pessoa jurídica.

**Request:**
```json
{
  "nmCliente": "Empresa XYZ",
  "email": "contato@xyz.com",
  "cnpj": "12.345.678/0001-99",
  "razaoSocial": "Empresa XYZ Ltda",
  "idAgencia": 1
}
```
**Response 201:**
```json
{
  "cnpj": "12.345.678/0001-99",
  "razaoSocial": "Empresa XYZ Ltda",
  "idCliente": 2,
  "nmCliente": "Empresa XYZ",
  "email": "contato@xyz.com",
  "tipoCliente": "PJ",
  "idAgencia": 1
}
```

---

### GET /api/Clientes/{id}
Busca cliente por ID.

**Response 200:**
```json
{
  "idCliente": 1,
  "nmCliente": "João Silva",
  "tipoCliente": "PF",
  "idAgencia": 1
}
```

---

### POST /api/Contratacoes
Solicita contratação de empréstimo. Para clientes PF, calcula o score automaticamente.

**Request:**
```json
{
  "idCliente": 1,
  "idProduto": 1
}
```
**Response 202:**
```json
{
  "idContratacao": 1,
  "status": "APROVADO",
  "score": 650
}
```

---

### GET /api/Contratacoes/{id}
Consulta status da contratação.

**Response 200:**
```json
{
  "idContratacao": 1,
  "idCliente": 1,
  "idProduto": 1,
  "status": "APROVADO",
  "dtSolicitacao": "2026-05-05T23:09:59"
}
```

## 7. Regra de Negócio — Score de Crédito

O score é calculado automaticamente para clientes PF no momento da contratação:

- Base: **500 pontos**
- Idade ≥ 30 anos: **+100 pontos**
- Idade ≥ 50 anos: **+100 pontos**
- Valor solicitado ≤ R$ 5.000: **+200 pontos**
- Valor solicitado ≤ R$ 20.000: **+100 pontos**
- Valor solicitado > R$ 20.000: **−200 pontos**

Score ≥ 500 → **APROVADO** | Score < 500 → **REJEITADO**

## 8. Evidências de Funcionamento

> Inserir prints do Swagger com os endpoints funcionando.

## 9. Persistência

- ORM: Entity Framework Core 8
- Banco: Oracle (oracle.fiap.com.br:1521/ORCL)
- Migrations aplicadas com sucesso via `Update-Database`
