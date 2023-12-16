import { ChangeEvent, ComponentProps, useState } from "react"
import api from "../api"
import CheckBox from '@react-native-community/checkbox';
import { View, Text, TouchableOpacity } from "react-native"


const Subtask = (props) => {
    const [subtask, setSubtask] = useState(props.subtask)

    const handlePress = async () => {
        const resp = await api.patch('/api/tasks/subtask/setcompleted', {
            subtaskID: subtask.id,
            taskID: props.ownerTaskID,
            completed: !subtask.completed
        })
        setSubtask(resp.data)
    }

    return <View style={subtask.completed ? {} : {backgroundColor: "#F5B7B1"}}>
        <TouchableOpacity onPress={handlePress}>
            <Text>{subtask.title}</Text>
        </TouchableOpacity>
        
    </View>
}

export default Subtask