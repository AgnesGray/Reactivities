import { useEffect } from 'react';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import LoadingComponent from './LoadingComponent';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';

function App() {
  const {activityStore} = useStore();

  useEffect(() => {
    activityStore.loadActivities();
  }, [activityStore]) //empty square parateses means this will execute only one time; 
         //if something is inside they're called dependencies and this is executed anytime dependencies change

  if(activityStore.loadingInitial) return <LoadingComponent content='Loading app'/>

  return (
    <>
      <NavBar/>
        <Container style={{marginTop: '7em'}}>         
          <ActivityDashboard />
        </Container>
    
    </>
  );
}

export default observer(App);
