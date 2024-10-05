# US 1019

Autor : 1221288

## 1. Context
This is the first time this task is assigned to be developed. 
This is a new functionality that allow Customer Manager To get an ordered list of candidates, using the job interview points (grades), to help me analyze the candidates.

## 2. Requirements

**US 1019** As Customer Manager, I want...

- 1019.1.  To get an ordered list of candidates, using the job interview points (grades), to help me analyze the candidates.
- Priority: 1
- References:  N/A

## 2.1. Client Clarifications


### Question 169    

> Relativamente a esta user story, "US 1019 - As
Customer Manager, I want to get an ordered list of candidates, using the job
interview points (grades), to help me analyze the candidates.", a lista que
pretende é relacionada a uma job opening correto? A maneira de ordenação é
ascendente ou quer uma opção que inclua ascendente e descendente?

Sim, a ordenação é relativa a candidaturas para um job opening. A ordenação deve
ser descendente, do que tem maior pontuação para o que tem menor pontuação.

### Question 177

> Na US 1019: As Customer Manager, I
want to get an ordered list of candidates, using the job interview points
(grades), to help me analyze the candidates. Pretende que para uma
determinada Job Opening de um cliente meu, retorno uma lista ordenada de
candidatos e suas notas da entrevista. Penso implementar essa funcionalidade
da seguinte forma:
Job Opening : XXX
Nome | Email | Grade
Jane Doe| jane@doe.pt | 85
John Doe | john@doe.pt | 70
Ou seja com ordenação descendente.
Conforme refere Q153 consegue ver numa instancia esta lista, e noutra instancia faz o
ranking que achar pertinente.
Acha bem?


Penso que queira fazer referência a Q163. Relativamente ao exemplo que apresenta
parece-me que satisfaz o que pretendo.

### Question 197

> Na questao Q169 é mencionado para a listagem ser ordenada descendentemente da nota da entrevista (como mencionado tambem na própria US), no entanto, a questão é, como idealiza a ordenação caso a job opening não possua entrevista?
Resposta

Esta US não faz sentido para processos que não tenham entrevista.

### Question 204

> Segundo a resposta A197, devemos então apenas permitir a listagem de job openings que tenham entrevista?

Penso que não percebi bem a referência à listagem de job openings. Esta US não faz sentido para job openings que não tenham entrevista, uma vez que assenta na listagem dos candidatos e dos seus pontos nas entrevista.

### Question 226

> Na descrição da user story : " As Customer Manager, I want to get an ordered list of candidates, using the job interview points (grades), to help me analyze the candidates". A intenção "analyze the candidates" impõe alguma mudança/remoção de candidatos no sistema?

A referência a “analize the candidates” é apenas para passar a ideia que se pretende nesta US que o sistema disponibilize uma forma do Customer Manager conseguir consultar o resultado das entrevistas de forma a ajudar a decidir o ranking dos candidatos. Nada mais. O ranking doa candidatos é registado no sistema através da US 1013

### Question 235

> Justificação de Notas Entrevista - Nesta user story , as notas da entrevista têm que ter obrigatoriamente uma justificação ?

Na secção 2.3.4: “The system should provide a justification, such as "A minimum Bachelor degree is required for the job position”. A similar approach is used for job interviews, but in this case, the goal is not to approve or reject a candidate but to evaluate the answers and calculate a grade for the interview in the range 1-100”. Sim, seria importante apresentar uma listagem ordenada pelas notas. Devia ainda ser possivel, para cada entrevista, saber a justfificação para a nota. Pode ser considerada justificação saber para cada pergunta a nota obtida e qual foi a resposta data pelo candidato (por exemplo).

### Question 236

> US 1019 Dúvida Fase analysis - Nesta user story , a expressão "to help me analyze candidates" ,na descrição da user story , impõe que a job Opening esteja na fase de análise ?

Não vejo isso como uma obrigação, mas penso que faz mais sentido nessa fase admitindo que apenas nessa fase seja garantido que todas as entrevistas foram efetuadas e todos os candidatos “avaliados” pelas entrevistas.

## 3. Analysis

### 3.1. Conditions

### 3.2. Use case diagram
[US_1019_UCD.puml](US_1019_UCD.puml)
## 4. Design

### 4.1. Applied Patterns

- **Repository:** This is used to store the users. This is done to allow the persistence of the enrollments and to allow the use of the enrollments in other parts of the application.
- **Service:** This is used to register the user in the system user repository. This is done to reduce coupling and to allow the use of the services in other parts of the application.

### 4.2. System Diagram
[US_1019_SD.puml](US_1019_SD.puml)
### 4.3 System Sequence Diagram
[US_1019_SSD.puml](US_1019_SSD.puml)

### 4.5. Tests

### Function Tests 4.5.1.

#### 4.5.1.1 List ordered candidates by interview grade

	1. Fazer o login como customer manager
	2. Escolher a opção   Customers >  Ordered list of candidates
	3. Mostra a lista de candidatos ordenada por ordem decrescente de notas de entrevista

## 5. Implementation

### 5.1. OrderedListOfCandidatesUI

```java
public class OrderedListOfCandidatesUI extends AbstractListUI<CandidateUser> {

    private final OrderedListCandidateController controller = new OrderedListCandidateController();
    @Override
    protected Iterable<CandidateUser> elements() {
        return controller.listCandidates();

    }

    @Override
    protected Visitor<CandidateUser> elementPrinter() {
        return new CandidatePrinter();
    }

    @Override
    protected String elementName() {
        return "Candidate";
    }

    @Override
    protected String listHeader() {
        return String.format("#  %-30s%-30s", "CANDIDATE", "GRADE");
    }


    @Override
    protected String emptyMessage() {
        return "No data";
    }

    @Override
    public String headline() {
        return "Ordered list of candidates";
    }
}

```

### 5.2. OrderedListCandidateController

```java
public class OrderedListCandidateController {
    private final AuthorizationService authz = AuthzRegistry.authorizationService();
    private final CandidateRepository candidateRepository = PersistenceContext.repositories().candidateUsers();

    public Iterable<CandidateUser> listCandidates() {
        authz.ensureAuthenticatedUserHasAnyOf(Jobs4uRoles.POWER_USER, Jobs4uRoles.CUSTOMER_MANAGER);

        return this.candidateRepository.findAll();
    }
}

```

### 5.3. Deactivate User Controller

```java

```

### 5.4. Deactivate User Controller

```java

```

### 5.5. Deactivate User Controller

```java

```

### 5.6. Deactivate User Controller

```java

```

### 5.7. Deactivate User Controller

```java

```

### 5.8. Deactivate User Controller

```java

```
### 5.1. Prototyping

## 6. Integration & Demonstration

### 6.1. Integration


## 7. Observations

- N/a