<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on }">
        <v-btn flat color="primary" dark v-on="on">
          <v-icon 
            small
            class="mr-1"
            @click="editItem(props.item)"
          >
            edit
          </v-icon>
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">Konfiguruoti vartotoją</span>
        </v-card-title>
        <v-card-text>
          <v-container grid-list-md>
            <v-layout wrap>
              <v-flex xs12>
                <v-text-field 
                v-model="userObj.userName"
                label="Prisijungimo vardas" 
                :value = "userObj.userName"  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field 
                v-model="userObj.name" 
                label="Vardas" 
                :value = "userObj.name"   outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field 
                v-model="userObj.surname"
                label="Pavardė" 
                :value = "userObj.surname"  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field 
                v-model="userObj.group"
                label="Grupė" 
                :value = "userObj.group"  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field 
                v-model="userObj.studentCode"
                label="Vidko" 
                :value = "userObj.studentCode"  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field 
                v-model="userObj.email"
                label="El. paštas" 
                :value = "userObj.email"  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field 
                v-model="userObj.uid"
                label="UID" 
                :value = "userObj.uid"  outline></v-text-field>
              </v-flex>
            </v-layout>
          </v-container>
          <!-- <small>*Būtini laukai</small> -->
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn dark color="blue darken-1" flat @click="dialog = false">Uždaryti</v-btn>
          <v-btn dark color="blue darken-1" flat @click="submit">Išsaugoti</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
  import { Device } from '../../models/Device';
  import { Classroom } from '../../models/Classroom';
  import { authHeader } from '../../components/login/auth-header';
  import { RegisteredUsers } from '../../models/RegisteredUsers';

  export default {
    name: 'EditUser',
    props: {
      userObj: Object,
    },
    data: () => ({
      //idx: null,
      //classrooms: [],
      //classroom: null,
      RegisteredUsers: null,
      users: [],
      idx: null,
      dialog: false
    }),
    created () {
      const headers = {...authHeader()};
      axios.get(`api/userlist/getusers`)
      .then(response => {
        // JSON responses are automatically parsed.
        //this.classrooms = response.data;

        //console.log(this.classrooms)
        // console.log(this.$props.userObj.classroomId);
        //this.idx =  findWithAttr(this.classrooms, 'classroomId', this.$props.userObj.classroomId);
        //this.classroom = this.classrooms[this.idx];
        //console.log(this.classroom);
        this.users = response.data;
        //console.log(this.users);
        this.idx = findWithAttr(this.users, 'studentCode', this.$props.userObj.studentCode);
        this.RegisteredUsers = this.users[this.idx];
        //console.log(this.idx);
        //console.log(this.RegisteredUsers);
      });
    },
    computed: {
      getUsers () {
        //console.log(this.users);
        return this.users;
      },
    },
    methods: {
      submit(){
          //this.$props.userObj.id = this.RegisteredUsers.id;
          //this.$props.userObj.name = this.RegisteredUsers.name;
          //this.$props.userObj.surname = this.RegisteredUsers.surname;
          //this.$props.userObj.studentCode = this.RegisteredUsers.studentCode;
          //this.$props.userObj.group = this.RegisteredUsers.group;
          //this.$props.userObj.email = this.RegisteredUsers.email;
          //this.$props.userObj.uid = this.RegisteredUsers.uid;
          this.$props.userObj
          //console.log(this.$props.userObj);
          const headers = {...authHeader()};
          axios.post('api/userlist/postuser/', this.$props.userObj, {headers:headers})
          .then((response) => {
            //this.
            //this.scans = response.data;
            //this.loading = false;
          });
          this.dialog = false;
      },
      getUser(id)
      {
        this.idx = findWithAttr(this.users, 'studentCode', this.$props.userObj.studentCode);
        //console.log(idx)
        //console.log(this.users[idx]);
        return idx;
      },
    }
}


function findWithAttr(array, attr, value) {
    for(var i = 0; i < array.length; i += 1) {
        if(array[i][attr] === value) {
            return i;
        }
    }
    return -1;
}
</script>