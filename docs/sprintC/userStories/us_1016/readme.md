# US 1016

Autor : 1221288

## 1. Context
This is the first time this task is assigned to be developed. 
This is a new functionality that allow the system  to notify candidates, by email, of the result of the verification process.

## 2. Requirements

**US 1016** As Customer Manager, I want ...

- 1016.1.  the system to notify candidates, by email, of the result of the verification process.
- Priority: 1
- References: See NFR11(RCOMP)

## 2.1. Client Clarifications


### Question 48

> US1016 e US1020, relativamente ao envio das notificações por
email, é necessário guardar que esse envio foi feito?

No documento nada de explicito é dito sobre este assunto. No entanto, do ponto de vista de gestão do processo da jobs4u parece-me adequado que essa informação fique registada

### Question 69

> Acerca da US 1016 - "As Customer Manager, I want the
system to notify candidates, by email, of the result of the verification process"
qual é o processo através do qual essa notificação é gerada? Após a
avaliação do Requirement Specification module, este gera um resultado
"Aprovado" ou "Rejeitado". Este resultado despoleta automaticamente uma
notificação para o candidato ou é o Customer Manager que tem a
responsabilidade de informar o candidato através do sistema do resultado da
verificação (ex. depois de um resultado negativo ser gerado, o Customer
Manager vai no sistema rejeitar o candidato para que seja enviado o email)?

É a segunda opção que apresenta. A US1015 permite que o Customer Manager
invoque o processo de verificação de requisitos. Depois disso todas as candidaturas
devem estar aceites ou recusadas. É então possível ao Customer Manager invocar a
notificação através da US1016.

### Question 164

> Em relação a US1016 que diz o seguinte: "As
Customer Manager, I want the system to notify candidates, by email, of the
result of the verifcation process". Eu gostaria de saber qual o formato da
mensagem a seguir para enviar ao cliente, poderia ser algo do genero: "Dear
[Candidate's Name], I hope this email finds you well. As the Customer
Manager, I wanted to inform you about the outcome of the verification
process for the position you applied for. After careful consideration of your
application and qualifications, I'm pleased to inform you that you have
successfully passed the verification process. Congratulations! Your application
has met our initial criteria, and we are impressed with your qualifications and
experience. We will be proceeding to the next phase of the selection process,
which may include interviews or additional assessments. We will reach out to
you soon with further details regarding the next steps. Thank you for your
interest in our company and for taking the time to apply for the position. We
appreciate your patience throughout the process. If you have any questions or
need further assistance, please don't hesitate to contact us. Best regards, [Your
Name] Customer Manager [Your Company Name]" Gostaria de saber as
informações mais importantes quando se notificar o candidato, deve aparecer
o nome do customer manager, a job reference, o nome do candidato. E se o
email deve ser em inglês ou português

Pode ser como apresenta. Pode ser em português ou inglês.

### Question 209

> About the Us1016 wich states: "As Customer Manager, I want the system to notify candidates, by email, of the result of verification process". I want to know when the client says "verification process" is the same about the screening phase.

Yes.

### Question 210

> This user story has a functional dependency with 1015. I would like to know if an error occurs, do I need to delete what happened in US 1015, as if it were a transaction?

The process of notification (US1016) must be done after the verification (US1015) but an error in the notification does not invalidate the “results” of the verification process.


## 3. Analysis

### 3.1. Conditions

### 3.2. Use case diagram

## 4. Design

### 4.1. Applied Patterns

- **Repository:** This is used to store the users. This is done to allow the persistence of the enrollments and to allow the use of the enrollments in other parts of the application.
- **Service:** This is used to register the user in the system user repository. This is done to reduce coupling and to allow the use of the services in other parts of the application.

### 4.2. System Diagram


### 4.3 System Sequence Diagram

### 4.4. Applied Patterns

### 4.5. Tests

### Function Tests 4.5.1.

### Unit Tests 4.5.2

## 5. Implementation

### 5.1. Prototyping

## 6. Integration & Demonstration

### 6.1. Integration


## 7. Observations

- N/a