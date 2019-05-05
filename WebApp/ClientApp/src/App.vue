<template>
  <v-app>

    <v-navigation-drawer v-if="isLoggedIn()" persistent :mini-variant="miniVariant" :clipped="clipped" v-model="drawer" enable-resize-watcher fixed app>
      <v-list>
        <v-list-tile value="true" v-for="(item, i) in items()" :key="i" :to="item.link">
          <v-list-tile-action>
            <v-icon v-html="item.icon"></v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title v-text="item.title"></v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>

    <v-toolbar  app :clipped-left="clipped">
      <v-toolbar-side-icon v-if="isLoggedIn()" @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-btn v-if="isLoggedIn()" icon @click.stop="miniVariant = !miniVariant">
        <v-icon v-html="miniVariant ? 'chevron_right' : 'chevron_left'"></v-icon>
      </v-btn>
      <v-btn v-if="isLoggedIn()" icon @click.stop="clipped = !clipped">
        <v-icon>web</v-icon>
      </v-btn>
      <v-toolbar-title v-text="title"></v-toolbar-title>
      <v-spacer></v-spacer>
      <Registration v-if="!isLoggedIn()" right/>
      <DialogTest v-if="!isLoggedIn()" right/>
      <v-chip v-if="isLoggedIn()" color="indigo" text-color="white">
        <v-avatar>
          <v-icon>account_circle</v-icon>
        </v-avatar>
        {{ myRole() }}
      </v-chip>
      <v-btn v-if="isLoggedIn()" icon @click.stop="logout">
        <v-icon>exit_to_app</v-icon>
      </v-btn>

    </v-toolbar>

    <v-content>
      <router-view/>
    </v-content>

    <v-footer app>
      <span>&nbsp;R.A.V.E.D&nbsp;&copy;&nbsp;2019&nbsp;&copy;&nbsp;v0.0.1</span>
    </v-footer>

  </v-app>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import DialogTest from './views/DialogTest.vue'; // @ is an alias to /src
import Registration from './views/Registration.vue';
import { userService } from './services/user.service';
//import './store/login/acount.module';
import { router } from './router';

@Component({
  components: {
    DialogTest,
    Registration,
  },
})
export default class App extends Vue {
  private clipped: boolean = true;
  private drawer: boolean = true;
  private miniVariant: boolean = false;
  private right: boolean = true;
  private title: string = 'NFC lankomumo sistema';
  // private userElement: string = JSON.parse(localStorage.getItem('user') || '{}');
  // private role: string = JSON.parse(localStorage.getItem('user') || '{}')['role']['roleId'];
  private renderComponent: boolean =  true;

  private itemsStudent = [
    { title: 'Pagrindinis', icon: 'home', link: '/home' },
    { title: 'Tvarkaraštis', icon: 'date_range', link: '/timetable' },
    { title: 'Statistika', icon: 'timeline', link: '/statistics' },
  ];
  private itemsAdmin = [
    { title: 'Pagrindinis', icon: 'home', link: '/home' },
    { title: 'Gauti duomenis (test)', icon: 'get_app', link: '/fetch-data' },
    { title: 'NFC skenavimai', icon: 'nfc', link: '/nfc-fetch' },
    { title: 'Naudotoju sąrašas', icon: 'supervised_user_circle', link: '/user-list' },
    { title: 'Tvarkaraščiai', icon: 'date_range', link: '/timetable' },
    { title: 'Įrenginiai', icon: 'scanner', link: '/device-fetch' },
    { title: 'Statistika', icon: 'timeline', link: '/statistics' },
  ];
  private itemsLecturer = [
    { title: 'Pagrindinis', icon: 'home', link: '/home' },
    { title: 'NFC skenavimai', icon: 'nfc', link: '/nfc-fetch' },
    { title: 'Naudotoju sąrašas', icon: 'supervised_user_circle', link: '/user-list' },
    { title: 'Tvarkaraščiai', icon: 'date_range', link: '/timetable' },
    { title: 'Statistika', icon: 'timeline', link: '/statistics' },
  ];

  private myRole()
  {
    const currentRole = JSON.parse(localStorage.getItem('user') || '{}')['role']['roleId'];
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    let myRoleString = '';
    if(currentRole === 'STUDENT')
    {
      myRoleString += 'Studentas';
    }
    if(currentRole === 'ADMIN')
    {
      myRoleString += 'Administratorius';
    }
    if(currentRole === 'LECTURER')
    {
      myRoleString += 'Dėstytojas';
    }
    myRoleString += ': ';
    myRoleString += user['userName']['name'] + ' ' + user['userName']['surname'];
    return myRoleString;
  }


  private items(){
    const currentRole = JSON.parse(localStorage.getItem('user') || '{}')['role']['roleId'];
    console.log('Role is:');
    console.log(currentRole);
    if(currentRole === 'STUDENT') 
    {
      return this.itemsStudent;
    }
    if(currentRole === 'ADMIN')
    {
      return this.itemsAdmin;
    }
    if(currentRole === 'LECTURER')
    {
      return this.itemsLecturer;
    }
  }

  private logout()
  {
    console.log('Pressed logout');
    userService.logout();
    router.push('/');
  }

  private logState()
  {
    this.$forceUpdate();
    // console.log(this.sss.userName.email);
    // console.log(this.sss.userName.group);
    // console.log(this.sss.userName.name);
    // console.log(this.sss.userName.surname);
    // console.log(this.sss.userName.id);
    // console.log(this.sss.userName.uid);
    // console.log(this.sss.userName.userName);
  }


  private isLoggedIn()
  {
    return localStorage.getItem('user');
  }
}

</script>

<style lang="css">
main.v-content {
  width: 100vw;
  height: calc(100vh - 64px);
  flex-direction: column;
  overflow: auto;
  margin-top: 64px;
  padding-top: 0 !important;
}
html {
  overflow: hidden;
}
</style>
