import { ChangeEvent, ComponentProps, useState } from "react"
import api from "../api"
import CheckBox from '@react-native-community/checkbox';
import { View, Text } from "react-native"


const Subtask = (props) => {
    const [subtask, setSubtask] = useState(props.subtask)

    const setCompleted = async (newValue) => {
        const resp = await api.patch('/api/tasks/subtask/setcompleted', {
            subtaskID: subtask.id,
            taskID: props.ownerTaskID,
            completed: newValue
        })
        setSubtask(resp.data)
    }

    console.log(subtask);

    return <View style={subtask.completed ? {} : {backgroundColor: "#F5B7B1"}}>
        {/* <CheckBox value={subtask.completed} onValueChange={v => setCompleted(v)}/> */}
        <Text>{subtask.title}</Text>
        
    </View>
}

export default Subtask