Rafael Autieri dos Anjos - rm550885
Henrique pontes Oliveira - rm98036
Rafael Carvalho Mattos - rm99874


# InnerHealth API

## Visão Geral

A InnerHealth API é uma aplicação RESTful desenvolvida em C#/.NET 8 para o projeto interdisciplinar “O Futuro do Trabalho”.  
O sistema registra hábitos de bem-estar e produtividade, oferecendo uma base sólida e didática para análises de rotina, saúde e desempenho.

A proposta central é simples: coletar pequenos hábitos do dia a dia e fornecer uma visão clara e objetiva sobre o equilíbrio entre vida pessoal e profissional.

---

## Funcionalidades

- **Perfil do Usuário**  
  Armazena peso, altura, idade, horas de sono e qualidade do sono. Essas informações servem como base para metas e recomendações.
- **Hidratação**  
  Registro de ingestões de água. A meta diária é calculada automaticamente: `peso × 35 ml`.
- **Exposição ao Sol**  
  Sessões diárias de exposição ao sol em minutos. Meta padrão: 10 minutos.
- **Meditação**  
  Sessões em minutos. Meta padrão: 5 minutos.
- **Sono**  
  Registros de horas dormidas e qualidade do sono.
- **Atividade Física**  
  Modalidade e duração.
- **Tarefas**  
  Criação, consulta, edição e exclusão de tarefas diárias.
- **Swagger**  
  Documentação automática disponibilizada em `/swagger`.
- **Versionamento da API**  
  Suporte às versões `v1` e `v2`, acessíveis via `/api/v1/...` e `/api/v2/...`.

---

## Arquitetura da Solução

A API segue uma estrutura organizada, clara e de fácil manutenção:

```
Cliente → Controllers → Services → Entity Framework Core → SQLite
```

- **Controllers:** responsáveis por receber chamadas HTTP e enviar respostas.
- **Services:** camada de regras de negócio.
- **DbContext:** interface direta com o banco, gerenciada pelo Entity Framework Core.
- **SQLite:** banco relacional em arquivo, adequado para projetos de estudo e demonstração.

O banco é criado automaticamente na primeira execução do projeto.

---

## Como Executar

### 1. Pré-requisitos

- .NET 8 SDK instalado.

Não é necessário instalar SQL Server ou qualquer outro sistema de banco de dados.

---

### 2. Clonar o Repositório

```
git clone <url-do-repositorio>
cd InnerHealth-backend/InnerHealth.Api
```

---

### 3. Executar a API

```
dotnet run
```

A API será iniciada em:

- `http://localhost:5000`  
- `http://localhost:5000/swagger`

O arquivo `InnerHealth.db` será criado automaticamente na pasta da aplicação.

---

## Versionamento e Diferenças Entre v1 e v2

A API utiliza versionamento explícito nas rotas:

```
/api/v1/...  
/api/v2/...
```

### v1 — Versão Base  
Contém toda a estrutura fundamental:
- CRUD de cada módulo
- Metas diárias básicas
- Operações de hoje e da semana
- Estrutura padrão do projeto

### v2 — Versão Expandida  
Inclui todos os recursos da v1 e acrescenta:
- Rotinas internas otimizadas
- Resumos diários mais completos
- Respostas mais descritivas e adequadas para dashboards
- Melhor organização dos serviços e cálculos agregados

Os endpoints permanecem iguais em nome e finalidade, mas a lógica interna da v2 é mais rica e preparada para futuras expansões

--

## Endpoints Principais

Para qualquer `{v}` sendo 1 ou 2:

### Perfil do Usuário
- GET `/api/v{v}/profile`  
- PUT `/api/v{v}/profile`

### Hidratação
- GET `/api/v{v}/water/today`  
- GET `/api/v{v}/water/week`  
- POST `/api/v{v}/water`  
- PUT `/api/v{v}/water/{id}`  
- DELETE `/api/v{v}/water/{id}`

### Sol, Meditação, Sono, Atividade Física
Padrão semelhante ao módulo de hidratação:
- `/today`
- `/week`
- POST, PUT, DELETE

### Tarefas
- GET `/api/v{v}/tasks/today`
- GET `/api/v{v}/tasks`
- POST `/api/v{v}/tasks`
- PUT `/api/v{v}/tasks/{id}`
- DELETE `/api/v{v}/tasks/{id}`

---

## Metas Diárias

- Água: `peso × 35 ml`
- Exposição ao Sol: 10 minutos
- Meditação: 5 minutos

As metas são utilizadas para guiar hábitos saudáveis e também como base para alertas na v2.

---

## Controle Diário

- Todas as datas utilizam `DateOnly`  
- Análises semanais seguem o modelo segunda–domingo  
- Dias sem registros retornam valores zerados ou nulos, facilitando plotagem de gráficos e indicadores

---

## Autenticação

A API não possui autenticação.  
O objetivo do projeto é demonstrar organização, boas práticas e clareza de código, sem adicionar complexidade que não seria utilizada na avaliação.

Possíveis evoluções:
- Autenticação baseada em JWT
- Suporte a múltiplos usuários
- Vínculo com aplicativos móveis

---

## Extensões Futuras

O sistema foi estruturado de forma a facilitar melhorias, como:

- Relatórios automáticos com auxílio de IA
- Dashboard analítico
- Aplicações mobile integradas
- Conexão com dispositivos e sensores
- Deploy em nuvem

-------------------------------------------------------



## Deploy em Cloud (Opcional)

O projeto pode ser executado em qualquer provedor que suporte Docker.  
Abaixo está um fluxo simples para subir a API em um servidor Linux (DigitalOcean, AWS EC2, Azure VM, Google VM, etc).

### 1. Subir os arquivos para o servidor
No servidor Linux, envie ou clone o repositório contendo:

```
Dockerfile  
docker-compose.yml  
entrypoint.sh  
```

Certifique-se de que estejam na raiz da API.

### 2. Instalar Docker e Docker Compose

```bash
sudo apt update
sudo apt install docker.io docker-compose -y
sudo systemctl enable docker
```

### 3. Construir e subir os containers

```bash
sudo docker-compose up -d --build
```

Esse comando:

- Faz pull da imagem do MySQL  
- Faz build da imagem da API  
- Inicia os containers  
- Mantém tudo rodando em modo daemon

### 4. Acessar a API na nuvem

Use o IP público do servidor:

```
http://SEU_IP_PUBLICO:8080
http://SEU_IP_PUBLICO:8080/swagger
```

### 5. Persistência e reinícios

- O MySQL usa o volume `mysql_data`, garantindo persistência dos dados.  
- A API utiliza `restart: always` e reinicia automaticamente.

### 6. (Opcional) HTTPS com Certbot

```bash
sudo apt install certbot
sudo certbot certonly --standalone -d seu-dominio.com
```

O Dockerfile já mapeia `/etc/letsencrypt` para dentro do container.

---

Esta etapa é opcional e serve como complemento profissional para demonstração de deploy em ambiente cloud.