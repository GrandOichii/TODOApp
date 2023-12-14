import { ComponentProps } from "react"

interface SubtaskProps extends ComponentProps<"div"> {
    subtask: Subtask
}

const Subtask = (props: SubtaskProps) => {

    const subtask = props.subtask
    return <>
        {subtask.title}
    </>
}

export default Subtask