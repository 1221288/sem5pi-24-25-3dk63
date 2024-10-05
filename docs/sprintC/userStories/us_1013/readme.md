# US 1013

Autor : 1211225

## 1. Context

This feature is being implemented for the first time

## 2. Requirements

**US 1013** As Customer Manager, I want to rank the candidates for a job opening.

- 1013.1. Create the method to select the job opening

- 1013.2. Create the method to display candidates

- 1013.3  Create the method to assign a ranking to a candiate

- 1013.4  Create the UI


## 2.1. Client Clarifications
> Q17 Beatriz – Relativamente à secção 2.2.1, é na fase de Analysis que as entrevistas são avaliadas e é esse resultado que define o ranking dos candidatos? Além disso, para que serve o CV nesta fase? Visto que as entrevistas não são obrigatórias, o que acontece quando estas não se realizam?

A17 A pontuação das entrevistas é efetuada/calculada na fase das entrevistas. O CV e outros dados (como o resultado das entrevistas) é usado pelo Customer manager na fase de analise para ordenar os candidatos. Mas a ordenação é da responsabilidade do Customer Manager (por exemplo, não tem de seguir a ordem da pontuação nas entrevistas). A US 1013 corresponde à ordenação manual dos candidatos feita pelo Customer Manager. O facto de não haver entrevistas não tem implicações na ordenação dos candidatos pois esta não depende explicitamente das entrevistas.

> Q142 Varela – US1013 - Candidate Ranking – Mr Client mention a manual ranking (Q17). If the pontuation of an interview is not essential for the candidate's rank, what criteria is to be used when ranking?

A142 Please view again Q17. The ranking is a decision of the Customer Manager based on all the data that he/she may have during the process (including CV and other attached documents and the interviews as well as other data/information that may not be part of the system).

> Q148 Pedro – US 1013 – A minha questão é relativa a como o ranking é feito. O customer manager dá uma nota a cada candidatura e o sistema ordena por ordem crescente sendo assim atribuído o ranking de cada candidatura? Se for assim, a nota terá que escala? Caso ainda seja assim ele só pode atribuir uma nota quando tiver conhecimento de todas? Ou pode ir colocando e o ranking só é atribuído quando todas as candidaturas já estiverem avaliadas?

A148 Ver Q17. A ordenação dos candidatos (ranking) é da responsabilidade do customer manager. Ele pode basear-se no resultado das entrevistas e de outra informação, mas o ranking não é automático. Não há nota nem escala a usar. As candidaturas são ordenadas.

> Q152 Costa – US 1013 - ranking scale - In US 1013, as mentioned before, the customer manager will decide the rank of each candidate's application for a job opening. As such, our team would like to know what is the scale of this rank and a bit of information about how it works. First of all, is the ranking a point system, in which the customer manager will award points to each application, making it so the the ranking is ordered by how many points each application has, or will the customer manager simply assign a place in the rank? For the second option, what I am picturing is the program asking the customer manager what position to place each candidate in, one by one, and the customer manager awarding, 1st, 2nd place and etc.. Second of all, if it is indeed a point system, is there a universal scale it should follow?

A152 See Q148.

> Q155 Varela – US1013 - Rank Entries - Is there a limit on rank entries? Let's say that 100 candidates apply for a job opening. Does the Customer Manager have to sort all 100 candidates?

A155 The order of candidates should include at least all the first candidates within the vacancy number and some following candidates. At the moment, I do not know exactly the number of the following candidates to be ordered. Therefore, I would like for it to be a global configuration property in the system. It could be a number representing a magnitude from the vacancy number. For instance, 1 could mean exactly the same number of vacancies, 2 the double, 0,5 half the number of vacancies. The remainder of the candidates could be just tagged as not ranked

> Q157 Miguel – US1013 - Rank the candidates for a job Opening is the same as rank the job Applications for a Job Opening, knowing that I can only know the candidates throw the job application?

A157 In the context of a job opening, a candidate is someone that made an application to that job opening. But the same person can be a candidate to other job openings

> Q158 Miguel – US1013 - Process of ranking - How is the ranking done? The customer manager selects a job opening and is shown the different candidates, and they assign a rank to each one. And the ranking process end when he assigns a rank to all candidates? Example: - Rank the candidate1: - Write the rank: 3 - Rank the candidate2: - Write the rank: 1 - Rank the candidate3: - Write the rank: 4

A158 See Q155. Once again, I do not have specific requirements for UI/UX. But I can provide some ideas. Being a console application limits the UI/UX. However, I see this functionality similar to the way people enter recipients for an email, for instance. In the case of the recipients of an email I simply write their emails separated by a comma. Could it be similar in this case?

> Q159 Miguel – US1013 - Stop the ranking process - When a customer manager starts the ranking process, he can stop and continue later? Or the ranking process must be done in one go?

A159 See Q158. I guess it may depend on how you implement the solution. But, in the case it may work as a “long operation” be aware of when and how to conclude the “operation”.

> Q160 Miguel – US1013 - Edit ranking - The customer manager can change the rank of a candidate after assigning it?

A160 See Q159. That should be possible if none of the interested parties were yet notified of the results.

> Q162 Miguel – US1013 - When the analysis phase ends, the ranking need to have all the candidates? or can the customer manager rank only some of the candidates?

A162 See Q149. All the candidates should be ranked before moving to the result phase

> Q163 Miguel – US1013 - When the customer manager is ranking the candidates, in terms of UI, should we display information from the application such as interview score, etc... or just the candidate's name and email?

A163 As stated before, I do not have specific requirements for the UI/UX. Use best practices. However, I would like it to be possible for the Customer Manager to have 2 or more instances of the application running, so that he/she could, for instance, see the interviews grades and, at the same time, register the order/ranking of the candidates.

> Q165 Varela – US1013 Clarifications - Mr. Client mentioned in Q155 that the system should have ranking configurations so that the Customer Manager doesn't have to rank all the candidates for a job opening, and that the ones that haven't been manually ranked are to be tagged with "not ranked". However, in Q162, you've said that all the candidates must be ranked before the result phase starts. Can you clarify this situation?

A165 The customer manager must evaluate all the candidates. It is the only way he/she can produce a ranking/order for the candidates and select the “best” candidates to be included in the vacancies for the job opening. In Q155 I was only proposing a way to avoid recording in the system a lot of details that will not have any impact on the next activities. The term “not ranked” maybe is not the best. Maybe “rank not recorded” or something similar could be more appropriated

> Q183 Costa – US1013 – documents - When ranking the candidates, the customer manager makes decisions based on their CV and other relevant documents. My question is: should there be an option for the customer manager to view the relevant documents before ranking a candidate? Or has the customer manager already made the decision before ranking the candidates and purely uses this functionality to assign the previously planned ranking?

A183. Once again this could be only a UX/UI issue. Please refer to Q17, Q163 and Q140. Also note that there is US1021 for listing application data.

> Q192 Varela – US1013 – Global Configurations – I'm struggling to understang the global configurations defined for US1013. All candidates must be ranked, so that all of them can be notified. Are the global configurations only for persistence purposes? Example: all candidates are ranked and notified, so if 0.5 is in the global definition, then only half of the number of vancancies must be persisted in the system. Is this the scenario that Mr. Client has in mind?

A192. I am assuming you refer to Q155. Suppose you have 2 vacancies. You have 80 candidates. If you configure the property as 1 you need to rank (record in the system) the 2 first candidates plus 2 other candidates. If the property is 0,5 you should rank the 2 first candidates plus 1 third candidate. If the property is 2, you should rank the 2 first candidates plus 4 extra candidates. This is a way to assure that you do not have to record in the system the rank of all the possible candidates, but only a number that includes the ones required to fulfill the vacancies plus some extra (according to the property) for possible exceptions (such as someone leaving the application). But my suggestion was only to help in the UI/UX. You may use any other option.

> Q198 Padilla – US1013 – The ranking of candidates should be save in an array to be able to do easier the result phase?

A198. This question is about the design/implementation of the solution. I have no specific opinion regarding this specific questio

> Q215 Araujo – US1013 – About the Us1013 which states: "As Customer Manager, I want to rank the candidates for a job opening.". I want to know if two candidates can be tied, which would mean having the same rank for the same job opening

A215. No, ties should not be allowed. It must be clear what candidates are selected to the vacancies.

## 3. Analysis

### 3.1. Conditions

- The customer Manager must be authenticated and authorized to perform the operations.
- Ranking is done manually by the Customer Manager
- Ranking cannot be done after entering the result phase
- Rankings can be changed at any time before the result phase
### 3.1. Domain Model

![domain model](../../global-artifacts/02.analysis/domain_model.png)

### 3.2. Use case diagram

![use case diagram](out/US1013_UCD.png "Use case diagram")


## 4. Design

### 4.1. Applied Patterns

- **Repository:** Used to save the ranking of applications

### 4.2. Sequence Diagram

![Sequence Diagram](out/US1013_SSD.png "System Sequence diagram")

## 5. Implementation
```java
package lapr4.jobs4u.app.backoffice.console.presentation.authz;

import eapli.framework.general.domain.model.EmailAddress;
import eapli.framework.infrastructure.authz.application.AuthorizationService;
import eapli.framework.infrastructure.authz.application.AuthzRegistry;
import lapr4.jobs4u.applicationmanagement.domain.Application;
import lapr4.jobs4u.applicationmanagement.repositories.ApplicationRepository;
import lapr4.jobs4u.infrastructure.persistence.PersistenceContext;
import lapr4.jobs4u.joboffermanagement.domain.*;
import lapr4.jobs4u.joboffermanagement.repository.ConfigurationRepository;
import lapr4.jobs4u.joboffermanagement.repository.JobOfferRepository;
import lapr4.jobs4u.joboffermanagement.repository.RankRepository;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.*;

public class RankCandidatesController {
    private final JobOfferRepository jobOfferRepository= PersistenceContext.repositories().jobOffers();
    private final ApplicationRepository applicationRepository = PersistenceContext.repositories().applications();
    private final AuthorizationService authz = AuthzRegistry.authorizationService();
    private final ConfigurationRepository configurationRepository = PersistenceContext.repositories().configurations();

    private final RankRepository rankRepository = PersistenceContext.repositories().ranks();

    public List<JobOffer> getMyJobOpenings() {
        EmailAddress operatorEmail = authz.session().get().authenticatedUser().email();
        Iterable<JobOffer> managerOffers = jobOfferRepository.findAllByManager(operatorEmail.toString());
        List<JobOffer> jobOffers = new ArrayList<>();

        for(JobOffer job: managerOffers) {
            Configuration configuration = configurationRepository.findByReference(job.getReference()).iterator().next();

            Calendar cal = Calendar.getInstance();
            Date ts = cal.getTime();
            Instant instant = Instant.ofEpochMilli(ts.getTime());
            LocalDateTime now = LocalDateTime.ofInstant(instant, ZoneId.systemDefault());

            if (job.getManagerEmail().equals(operatorEmail.toString())
                && configuration != null
                && configuration.getAnalysisPhase().getPeriod().getEndDate().isBefore(now)
            ) {

                jobOffers.add(job);
                }
            }

        return jobOffers;
    }

    public List<Application> getJobOfferApplications(Reference reference){
        Iterable<Application> jobApplications = applicationRepository.findByReference(reference);

        List<Application> applications = new ArrayList<>();

        for(Application a : jobApplications){
            applications.add(a);
        }

        return applications;
    }

    public void saveRanking(List<Order> ranking,JobOffer toRank){
        toRank.replaceIsRanked(true);
        jobOfferRepository.save(toRank);
        Rank rank = new Rank(ranking,toRank);
        rankRepository.save(rank);
    }

    public Iterable<Rank> hasRanking(JobOffer jobOffer){
        return rankRepository.findByJobOffer(jobOffer);
    }

    public void deleteRanking(JobOffer offer){
        Iterable<Rank> ranking = rankRepository.findByJobOffer(offer);
        Rank rank = null;
        for(Rank r : ranking){
            rank=r;
        }
        rankRepository.delete(rank);
    }
}

```

## 6. Integration/Demonstration
### 6.1. Create New ranking
![addCandidate.png](out/ranking.png)
### 6.2 Maintain already saved ranking
![addCandidate.png](out/no_change.png)
## 7. Observations

- N/a