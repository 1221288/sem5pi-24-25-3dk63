# US 1008

Author : 1221265

## 1. Context

It is the first time this task is being developed.


## 2. Requirements

_In this section you should present the functionality that is being developed, how do you understand it, as well as possible correlations to other requirements (i.e., dependencies)._

**US 1008** As Language Engineer, I want to deploy and configure a plugin (i.e., Job Requirement Specification or Interview Model) to be used by the system.

- 1008.1. In any of the plugins, the type of questions that must be supported is what is presented on page 8 of the document (Q121) .
- Priority: 1
- References: See NFR09(LPROG)


## 3. Analysis

_In this section, the team should report the study/analysis/comparison that was done in order to take the best design decisions for the requirement.
This section should also include supporting diagrams/artifacts (such as domain model; use case diagrams, etc.),_

### 3.1. SSD

![ssd](out/US1008_SSD.svg "SSD")

### 3.2. Use case diagram

![use case diagram](out/US1008_UCD.svg "Use case diagram")


## 4. Design

_In this sections, the team should present the solution design that was adopted to solve the requirement.
This should include, at least, a diagram of the realization of the functionality (e.g., sequence diagram),
a class diagram (presenting the classes that support the functionality),
the identification and rational behind the applied design patterns and the specification of the main tests used to validade the functionality._

### 4.1. Applied Patterns
- **Controller:** This is used to handle user inputs and call the appropriate functionality while separating the UI from the rest of the application
- **Repository:** This is used to store the users. This is done to allow the persistence of the enrollments and to allow the use of the enrollments in other parts of the application.
- **Service:** This is used to register the user in the system user repository. This is done to reduce coupling and to allow the use of the services in other parts of the application.

## 5. Implementation

- Important commits:
    - 3bca5de07207e44fcec0946efe79908ccd6e6b52 : First commit of the US
    - 8174b351f721529a8f3353aadbfd4f175735c123 : Last commit of the US

## 6. Integration/Demonstration

### 6.1. Importing Job Requirements Plugin

![importing_requirement](out/importing_requirement.png)


### 6.2. Importing Interview Model Plugin

![importing_interview](out/importing_interview.png)

## 7. Observations

- N/a
