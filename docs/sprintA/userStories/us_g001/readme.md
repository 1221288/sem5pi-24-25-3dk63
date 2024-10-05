# US G001

*As Project Manager, I want the team to follow the technical constraints and concerns of the project*

## 1. Context

*This is the first time the task is assined to be developed*

## 2. Requirements

**US G001** As Project Manager, I want the team to follow the technical constraints and concerns of the project.

**Acceptance Criteria:**

- **Programming language:** The solution should be implemented using Java as
  the main language. Other languages can be used in accordance with more specific requirements.

- **Technical Documentation:** Project documentation should be always available
  on the project repository and, when applicable, in accordance to the UML notation. The development process of every US (e.g.: analysis,
  design, testing, etc.) must be reported (as part of the documentation).

- **Source Control:** The source code of the solution as well as all the documentation and related artifacts should be versioned in a GitHub repository to be provided
  to the students. Only the main branch will be used (e.g., as a source for
  releases).

- **Deployment and Scripts:** The repository should include the necessary scripts
to build and deploy the solution in a variety of systems (at least Linux and Windows). It
should also include a readme.md file in the root folder explaining how to build, deploy
and execute the solution.

- **Database:** By configuration, the system must support that data persistence
  is done either "in memory" or in a relational database (RDB).The system
  should have the ability to initialize some default data.

- **Continuous Integration:** The Github repository will provide night builds with
  publishing of results and metrics.

  
## 3. Implementation

- The project hast the necessary scripts for this project were based on the scripts from the example eapli base project, available at https://moodle.isep.ipp.pt/mod/resource/view.php?id=193762.
Some folders/packages were renamed to fit this project's requirements.

- The project is implemented in Java, as requested. The project is versioned in a GitHub repository, available at https://github.com/Departamento-de-Engenharia-Informatica/sem4pi-23-24-2dk3.

- The project uses a relational database (H2) and the system has the ability to initialize some default data.

- The project has a readme.md file in the root folder explaining how to build, deploy and execute the solution.

- The project has a GitHub Actions workflow that provides night builds with publishing of results and metrics.

- The project has a technical documentation available on the project repository and, when applicable, in accordance to the UML notation. The development process of every US (e.g.: analysis, design, testing, etc.) is reported (as part of the documentation).



