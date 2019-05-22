<template>
  <v-layout class="ma-2">
    <v-flex xs6 sm7 offset-sm2>
      <v-card>
        <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">Varotojo nustatymai</h3>
          </div>
        </v-card-title>
        <v-card-text>
          <v-container >
            <v-layout wrap>
              <v-flex xs12>
                <v-text-field v-model="userObj.userName" label="Prisijungimo vardas" outline></v-text-field>
              </v-flex>
              <v-flex>
                <v-text-field v-model="userObj.name" label="Vardas" :value="userObj.name" outline></v-text-field>
              </v-flex>
              <v-flex>
                <v-text-field
                  v-model="userObj.surname"
                  label="Pavardė"
                  :value="userObj.surname"
                  outline
                ></v-text-field>
              </v-flex>
              <v-flex >
                <v-text-field v-model="userObj.group" label="Grupė" :value="userObj.group" outline></v-text-field>
              </v-flex>
              <v-flex >
                <v-text-field
                  v-model="userObj.studentCode"
                  label="Vidko"
                  :value="userObj.studentCode"
                  outline
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="userObj.email"
                  label="El. paštas"
                  :value="userObj.email"
                  outline
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field v-model="userObj.uid" label="UID" :value="userObj.uid" outline></v-text-field>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>

        <v-card-actions>
          <v-btn flat color="primary">Pateikti</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import { Device } from "../models/Device";
import { Classroom } from "../models/Classroom";
import { authHeader } from "../components/login/auth-header";
import { RegisteredUsers } from "../models/RegisteredUsers";

export default {
  name: "EditUser",
  props: {
    userObj: Object
  },
  data: () => ({
    RegisteredUsers: null,
    users: [],
    idx: null,
    dialog: false
  }),
  created() {
    const currentUser = JSON.parse(localStorage.getItem("user"))["userName"];
    this.$props.userObj = currentUser;
    console.log(this.$props.userObj);
  },
  methods: {
    submit() {
      //this.$props.userObj.id = this.RegisteredUsers.id;
      //this.$props.userObj.name = this.RegisteredUsers.name;
      //this.$props.userObj.surname = this.RegisteredUsers.surname;
      //this.$props.userObj.studentCode = this.RegisteredUsers.studentCode;
      //this.$props.userObj.group = this.RegisteredUsers.group;
      //this.$props.userObj.email = this.RegisteredUsers.email;
      //this.$props.userObj.uid = this.RegisteredUsers.uid;
      this.$props.userObj;
      //console.log(this.$props.userObj);
      const headers = { ...authHeader() };
      axios
        .post("api/userlist/postuser/", this.$props.userObj, {
          headers: headers
        })
        .then(response => {
          //this.
          //this.scans = response.data;
          //this.loading = false;
        });
      this.dialog = false;
    },
  }
};
</script>