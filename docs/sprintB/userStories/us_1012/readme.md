# US 1012

Author : 1221959

## 1. Context

It is the first time this task is being developed.


## 2. Requirements

**US 1012** As Customer Manager, I want to generate and export a template text file to help
collect the candidate answers during the interviews.

- Priority: 1
- References: Alternatively this can be achieved by a bootstrap process
- The support for this functionality must follow specific technical requirements, specified in LPROG. The ANTLR tool should be used (https://www.antlr.org/).

## 3. Analysis

### 3.1. Domain Model

![domain model](out/US1001_DM.svg "Domain Model")

### 3.2. Use case diagram

![use case diagram](out/US1001_UCD.svg "Use case diagram")


## 4. Design
#### 4.4.1.1 Register job offer
![login[.png](assets%2Flogin%5B.png)

```
1-entrar como customerManager
2-selecionar a opção 2
3-selecionar a opção 7
4- inserir os dados pedidos
5. Em caso de sucesso, o template foi gerado foi criada.
```
### 4.1. Applied Patterns

- **Service:**  This pattern is used to separate the business logic from the presentation logic. This pattern is used to create a service that will be responsible for generating the template file.
- **GenerateInterviewTemplateController** This class is responsible for controlling the generation of the template file. It will receive the necessary information from the user and call the service to generate the file.

## 5. Implementation
```java
public class GenerateInterviewTemplateController {

    private final InterviewModelRepository interviewModelRepository = PersistenceContext.repositories().interviewModels();
    private final JobOfferRepository jobOfferRepository = PersistenceContext.repositories().jobOffers();

    private final ExporterService service = new ExporterService();

    private final AuthorizationService authorizationService = AuthzRegistry.authorizationService();


    private String getManagerEmail() {
        authorizationService.ensureAuthenticatedUserHasAnyOf(Jobs4uRoles.POWER_USER, Jobs4uRoles.CUSTOMER_MANAGER);
        Optional<UserSession> userSessionOptional = authorizationService.session();
        UserSession userSession = userSessionOptional.get();
        SystemUser authenticatedUser = userSession.authenticatedUser();
        return authenticatedUser.email().toString();
    }


    //guardar numa lista as references dos joboffers
    public List <String> GetJobOffersReferences(){
        Iterable<JobOffer> listaJobOffers =  jobOfferRepository.findAllByManager(getManagerEmail());
        List <String> references = new ArrayList<>();

        //references das job offers do manager
        for (JobOffer jobOffer : listaJobOffers) {
            references.add(jobOffer.getReference().toString());
        }
        return references;
    }

    public List <String> GetInterviewModelsReferences(){
        Iterable<InterviewModel> listaInterview =  interviewModelRepository.findAll();
        List <String> references = new ArrayList<>();

        //references das job offers do manager
        for (InterviewModel interviewModel : listaInterview) {
            references.add(interviewModel.getReference().toString());
        }

        return references;
    }

    public List <String> checkInterviewModels(){
        List <String> references = GetJobOffersReferences();
        List <String> referencesInterview = GetInterviewModelsReferences();

        List <String> JobOfferWithInterviewModel= new ArrayList<>();
        //verificar se já existe um modelo de entrevista para cada job offer
        for (String reference : references) {
            if (referencesInterview.contains(reference)){
                JobOfferWithInterviewModel.add(reference);
            }
        }
        return JobOfferWithInterviewModel;
    }

    public boolean generateInterviewTemplate(Reference reference) {
        // Encontrar o modelo de entrevista com base na referência
        InterviewModel interviewModel = interviewModelRepository.findByReference(reference).iterator().next();

        String filename =interviewModel.getQuestionForm().filePath();
        System.out.println("\n\n"+filename);
        return service.InterviewModelService(filename);
    }
}
```
## 6. Integration/Demonstration
![generate.png](assets%2Fgenerate.png)
![template.png](assets%2Ftemplate.png)
## 7. Observations

- N/a