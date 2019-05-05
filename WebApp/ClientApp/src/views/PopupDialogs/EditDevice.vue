<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on }">
        <v-btn color="primary" dark v-on="on">
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
          <span class="headline">Konfiguruoti įrenginį</span>
        </v-card-title>
        <v-card-text>
          <v-container grid-list-md>
            <v-layout wrap>
              <v-flex xs12>
                <v-text-field label="Sąrašo nr." :value = "deviceObj.deviceId" required readonly  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field label="Realus įrenginio ID" :value = "deviceObj.deviceIdReal"  required readonly  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field label="Laukiama atnaujinimo" :value = "deviceObj.updatePending" required readonly  outline></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field v-model.number="deviceObj.pendingDeviceId" label="Naujas įrenginio ID*" :value = "deviceObj.pendingDeviceId"></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-select
                  label="Auditorija*"
                  v-model="classroom"
                  :items="classrooms"
                  name="classroom"
                  :item-text="textProps"
                  required
                  return-object
                >
                  
                </v-select>
              </v-flex>
            </v-layout>
          </v-container>
          <small>*Būtini laukai</small>
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

  export default {
    name: 'EditDevice',
    props: {
      deviceObj: Object,
    },
    data: () => ({
      idx: null,
      classrooms: [],
      classroom: null,
      dialog: false
    }),
    created () {
      const headers = {...authHeader()};
      axios.get(`api/lectures/getclassrooms`)
      .then(response => {
        // JSON responses are automatically parsed.
        this.classrooms = response.data;
        console.log(this.classrooms)
        // console.log(this.$props.deviceObj.classroomId);
        this.idx =  findWithAttr(this.classrooms, 'classroomId', this.$props.deviceObj.classroomId);
        this.classroom = this.classrooms[this.idx];
        console.log(this.classroom);
        console.log(this.idx);
      });
    },
    computed: {
      getClassrooms () {
        console.log(this.classrooms);
        return this.classrooms;
      },

      getNormalLocation(label, location){
        return label + " " + location;
      }
      
    },
    methods: {
      submit(){
          this.$props.deviceObj.classroomId = this.classroom.classroomId;
          this.$props.deviceObj
          console.log(this.$props.deviceObj);
          const headers = {...authHeader()};
          axios.post('api/nfcscan/postdevice/', this.$props.deviceObj, {headers:headers})
          .then((response) => {
            this.scans = response.data;
            this.loading = false;
          });
          this.dialog = false;
      },
      getClass(id)
      {
        var idx =  findWithAttr(this.classrooms, 'classroomId', this.$props.deviceObj.classroomId)
        console.log(this.$props.deviceObj.classroomId);
        console.log(idx)
        console.log(this.classrooms[idx]);
        var classss = this.classrooms[idx];
        //this.classroom = classss.classroomId + ' - ' + classss.classLocation + ' r.-' + classss.classLabel;
        // console.log(this.classrooms[idx]);
        // console.log(this.classroom);
        //this.classroom = this.classrooms[idx]
        return idx;
      },
      textProps: item => item.classroomId + ' - ' + item.classLocation + ' r.-' + item.classLabel,
    }
    
}

function getTextProps(item)
{
  return item.classroomId + ' - ' + item.classLocation + ' r.-' + item.classLabel;
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