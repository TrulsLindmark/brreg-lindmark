import { createFileRoute } from "@tanstack/react-router";
import { EmployeeDistribution } from "../components/EmployeeDistribution/EmployeeDistribution";
import { OrganizationTypeDistribution } from "../components/OrganizationTypeDistribution/OrganizationTypeDistribution";
import { StatusDistribution } from "../components/StatusDistribution/StatusDistribution";
import { useFetchCompanySummary } from "../data/api/hooks";
import "./index.scss";

export const Route = createFileRoute("/")({
  component: HomePage,
});

function HomePage() {
  const { data: companySummary } = useFetchCompanySummary();

  if (companySummary == null) {
    return;
  }

  return (
    <div className="home-page">
      <div className="home-page__dashboard">
        <div>
          <StatusDistribution statusDistribution={companySummary.statusCount} />
        </div>
        <div>
          <OrganizationTypeDistribution
            organizationTypeDistribution={
              companySummary.organizationTypeDistribution
            }
          />
        </div>
        <div>
          <EmployeeDistribution
            employeeDistribution={companySummary.employeeDistribution}
          />
        </div>
      </div>
    </div>
  );
}
