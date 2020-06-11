using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//                            p1---p2
//                            |    |     <- head
//                            p3---p4
//                            └-p5-┘             
//                              |          <- neck
//  hand  underarm overarm     p6
// p32--p30---p28--┐        /       \       ┌--p35---------p37---p39
// |     |     |   p27----p8  chest p9----p34   |          |      |
// p33--p31---p29--┘        \       /       └--p36---------p38---p40
//                              p7
//                               |
//                              p10         <- hip
//                            /     \   
//                          /    ↑y axis
// ------------------------p11---*---p12-------------------> x axis
//                         |     |     |
//          left thigh->   p13       p14     <- thigh
//                         /  \      /  \
//                        /    \    /    \
//                       p15--p16   p17--p18
//    left underleg->    |      |   |      |
//                       |      |   |      | <- under leg
//                       p19--p20   p21--p22
//                       |      |   |      |
//        left foot->    |      |   |      |<- foot
//                       p23--p24   p25--p26



public class RagdollNewBallJoint : MonoBehaviour
{
  public float head_y = 10;
  public float neck_y = 3;
  public float chest_y = 17;
  public float hip_y = 10;
  public float thigh_y = 20;
  public float underleg_y = 20;
  public float feet_y = 5;
  public float overarm_y = 5;
  public float underarm_y = 5;
  public float hand_y = 5;

  public float head_x = 10;
  public float neck_x = 10;
  public float chest_x = 15;
  public float hip_x = 15;
  public float thigh_x = 5;
  public float underleg_x = 5;
  public float feet_x = 5;
  public float overarm_x = 15;
  public float underarm_x = 15;
  public float hand_x = 5;

  public float x_base = 0;
  public float y_base = 0;
  public float z_base = 0;
  public float z_depth = 5;
  public float cube_scale = 0.9f;

  Ragdoll ragdoll;

  RagdollBone head;
  RagdollBone neck;
  RagdollBone chest;
  RagdollBone hip;
  RagdollBone left_thigh;
  RagdollBone right_thigh;
  RagdollBone left_underleg;
  RagdollBone right_underleg;
  RagdollBone left_feet;
  RagdollBone right_feet;
  RagdollBone left_overarm;
  RagdollBone left_underarm;
  RagdollBone left_hand;
  RagdollBone right_overarm;
  RagdollBone right_underarm;
  RagdollBone right_hand;

  MyHingeJoint head_to_neck;
  MyBallJoint neck_to_chest;
  MyBallJoint chest_to_hip;
  MyBallJoint left_hip_to_thigh;
  MyBallJoint right_hip_to_thigh;
  MyHingeJoint left_thigh_to_underleg;
  MyHingeJoint left_underleg_to_feet;
  MyHingeJoint right_thigh_to_underleg;
  MyHingeJoint right_underleg_to_feet;
  MyBallJoint left_chest_to_overarm;
  MyBallJoint right_chest_to_overarm;
  MyHingeJoint left_overarm_to_underarm;
  MyHingeJoint right_overarm_to_underarm;
  MyHingeJoint left_arm_to_hand;
  MyHingeJoint right_arm_to_hand;

  // Start is called before the first frame update
  void Start()
  {
    Particle p1 = new Particle();
    Particle p2 = new Particle();
    Particle p3 = new Particle();
    Particle p4 = new Particle();
    Particle p5 = new Particle();
    Particle p6 = new Particle();
    Particle p7 = new Particle();
    Particle p8 = new Particle();
    Particle p9 = new Particle();
    Particle p10 = new Particle();
    Particle p11 = new Particle();
    Particle p12 = new Particle();
    Particle p13 = new Particle();
    Particle p14 = new Particle();
    Particle p15 = new Particle();
    Particle p16 = new Particle();
    Particle p17 = new Particle();
    Particle p18 = new Particle();
    Particle p19 = new Particle();
    Particle p20 = new Particle();
    Particle p21 = new Particle();
    Particle p22 = new Particle();
    Particle p23 = new Particle();
    Particle p24 = new Particle();
    Particle p25 = new Particle();
    Particle p26 = new Particle();
    Particle p27 = new Particle();
    Particle p28 = new Particle();
    Particle p29 = new Particle();
    Particle p30 = new Particle();
    Particle p31 = new Particle();
    Particle p32 = new Particle();
    Particle p33 = new Particle();
    Particle p34 = new Particle();
    Particle p35 = new Particle();
    Particle p36 = new Particle();
    Particle p37 = new Particle();
    Particle p38 = new Particle();
    Particle p39 = new Particle();
    Particle p40 = new Particle();


    float under_limbs_y = feet_y + underleg_y + thigh_y;
    float mid_limbs_y = hip_y + chest_y;
    float upper_limbs_y = neck_y + head_y;
    float left_limbs_x = hand_x + underarm_x + overarm_x;
    float right_limbs_x = left_limbs_x;

    p1.set_init_position(-head_x / 2, mid_limbs_y + upper_limbs_y, 0);
    p2.set_init_position(head_x / 2, mid_limbs_y + upper_limbs_y, 0);
    p3.set_init_position(-head_x / 2, mid_limbs_y + neck_y, 0);
    p4.set_init_position(head_x / 2, mid_limbs_y + neck_y, 0);
    p5.set_init_position(0, mid_limbs_y + neck_y / 2, 0);
    p6.set_init_position(0, mid_limbs_y, 0);
    p7.set_init_position(0, hip_y, 0);
    p8.set_init_position(-chest_x / 2, hip_y + chest_y / 2, 0);
    p9.set_init_position(chest_x / 2, hip_y + chest_y / 2, 0);
    p10.set_init_position(0, hip_y / 2, 0);
    p11.set_init_position(-hip_x / 2, 0, 0);
    p12.set_init_position(hip_x / 2, 0, 0);
    p13.set_init_position(-hip_x / 2, -thigh_y / 2, 0);
    p14.set_init_position(hip_x / 2, -thigh_y / 2, 0);
    p15.set_init_position(-(hip_x + thigh_x) / 2, -thigh_y, 0);
    p16.set_init_position(-(hip_x - thigh_x) / 2, -thigh_y, 0);
    p17.set_init_position((hip_x - thigh_x) / 2, -thigh_y, 0);
    p18.set_init_position((hip_x + thigh_x) / 2, -thigh_y, 0);
    p19.set_init_position(-(hip_x + underleg_x) / 2, -thigh_y - underleg_y, 0);
    p20.set_init_position(-(hip_x - underleg_x) / 2, -thigh_y - underleg_y, 0);
    p21.set_init_position((hip_x - underleg_x) / 2, -thigh_y - underleg_y, 0);
    p22.set_init_position((hip_x + underleg_x) / 2, -thigh_y - underleg_y, 0);
    p23.set_init_position(-(hip_x + feet_x) / 2, -under_limbs_y, 0);
    p24.set_init_position(-(hip_x - feet_x) / 2, -under_limbs_y, 0);
    p25.set_init_position((hip_x - feet_x) / 2, -under_limbs_y, 0);
    p26.set_init_position((hip_x + feet_x) / 2, -under_limbs_y, 0);
    p27.set_init_position(-(chest_x + overarm_x) / 2, hip_y + chest_y / 2, 0);
    p28.set_init_position(-(overarm_x + chest_x / 2), hip_y + (chest_y + overarm_y) / 2, 0);
    p29.set_init_position(-(overarm_x + chest_x / 2), hip_y + (chest_y - overarm_y) / 2, 0);
    p30.set_init_position(-(underarm_x + overarm_x + chest_x / 2), hip_y + (chest_y + underarm_y) / 2, 0);
    p31.set_init_position(-(underarm_x + overarm_x + chest_x / 2), hip_y + (chest_y - underarm_y) / 2, 0);
    p32.set_init_position(-(hand_x + underarm_x + overarm_x + chest_x / 2), hip_y + (chest_y + hand_y) / 2, 0);
    p33.set_init_position(-(hand_x + underarm_x + overarm_x + chest_x / 2), hip_y + (chest_y - hand_y) / 2, 0);
    p34.set_init_position((chest_x + overarm_x) / 2, hip_y + chest_y / 2, 0);
    p35.set_init_position(chest_x / 2 + overarm_x, hip_y + (chest_y + overarm_y) / 2, 0);
    p36.set_init_position(chest_x / 2 + overarm_x, hip_y + (chest_y - overarm_y) / 2, 0);
    p37.set_init_position(chest_x / 2 + overarm_x + underarm_x, hip_y + (chest_y + underarm_y) / 2, 0);
    p38.set_init_position(chest_x / 2 + overarm_x + underarm_x, hip_y + (chest_y - underarm_y) / 2, 0);
    p39.set_init_position(chest_x / 2 + overarm_x + underarm_x + hand_x, hip_y + (chest_y + hand_y) / 2, 0);
    p40.set_init_position(chest_x / 2 + overarm_x + underarm_x + hand_x, hip_y + (chest_y - hand_y) / 2, 0);

    ragdoll = new Ragdoll();

    head = new RagdollBone(true);
    head.init(ragdoll, p1, p2, p3, p4, "head");
    head.set_cube_pos(head_x / 2, head_y / 2, 0);
    head.set_cube_size(head_x, head_y, z_depth, cube_scale);
    neck = new RagdollBone(true);
    neck.init(ragdoll, p3, p4, p5, p6, "neck");
    neck.set_cube_pos(neck_x / 2, neck_y / 2, 0);
    neck.set_cube_size(neck_x / 2, neck_y, z_depth, cube_scale);
    chest = new RagdollBone(true);
    chest.init(ragdoll, p8, p9, p6, p7, "chest");
    chest.set_cube_pos(chest_x / 2, 0, 0);
    chest.set_cube_size(chest_x, chest_y, z_depth, cube_scale);
    hip = new RagdollBone(true);
    hip.init(ragdoll, p11, p12, p7, p10, "hip");
    hip.set_cube_pos(hip_x / 2, hip_y / 2, 0);
    hip.set_cube_size(hip_x, hip_y, z_depth, cube_scale);
    left_thigh = new RagdollBone(true);
    left_thigh.init(ragdoll, p15, p16, p11, p13, "left thigh");
    left_thigh.set_cube_pos(thigh_x / 2, thigh_y / 2, 0);
    left_thigh.set_cube_size(thigh_x, thigh_y, z_depth, cube_scale);
    right_thigh = new RagdollBone(true);
    right_thigh.init(ragdoll, p17, p18, p12, p14, "right thigh");
    right_thigh.set_cube_pos(thigh_x / 2, thigh_y / 2, 0);
    right_thigh.set_cube_size(thigh_x, thigh_y, z_depth, cube_scale);
    left_underleg = new RagdollBone(true);
    left_underleg.init(ragdoll, p15, p16, p19, p20, "left underleg");
    left_underleg.set_cube_pos(underleg_x / 2, underleg_y / 2, 0);
    left_underleg.set_cube_size(underleg_x, underleg_y, z_depth, cube_scale);
    right_underleg = new RagdollBone(true);
    right_underleg.init(ragdoll, p17, p18, p21, p22, "right underleg");
    right_underleg.set_cube_pos(underleg_x / 2, underleg_y / 2, 0);
    right_underleg.set_cube_size(underleg_x, underleg_y, z_depth, cube_scale);
    left_feet = new RagdollBone(true);
    left_feet.init(ragdoll, p19, p20, p23, p24, "left feet");
    left_feet.set_cube_pos(feet_x / 2, feet_y / 2, 0);
    left_feet.set_cube_size(feet_x, feet_y, z_depth, cube_scale);
    right_feet = new RagdollBone(true);
    right_feet.init(ragdoll, p21, p22, p25, p26, "right feet");
    right_feet.set_cube_pos(feet_x / 2, feet_y / 2, 0);
    right_feet.set_cube_size(feet_x, feet_y, z_depth, cube_scale);
    // x axis and y axis are rotated in local coordinate system
    left_overarm = new RagdollBone(true);
    left_overarm.init(ragdoll, p28, p29, p27, p8, "left overarm");
    left_overarm.set_cube_pos(overarm_y / 2, overarm_x / 2, 0);
    left_overarm.set_cube_size(overarm_x, overarm_y, z_depth, cube_scale);
    left_underarm = new RagdollBone(true);
    left_underarm.init(ragdoll, p31, p29, p28, p30, "left underarm");
    left_underarm.set_cube_pos(underarm_x / 2, underarm_y / 2, 0);
    left_underarm.set_cube_size(overarm_x, overarm_y, z_depth, cube_scale);
    left_hand = new RagdollBone(true);
    left_hand.init(ragdoll, p33, p31, p32, p30, "left hand");
    left_hand.set_cube_pos(hand_x / 2, hand_y / 2, 0);
    left_hand.set_cube_size(hand_x, hand_y, z_depth, cube_scale);
    // x axis and y axis are rotated in local coordinate system
    right_overarm = new RagdollBone(true);
    right_overarm.init(ragdoll, p35, p36, p9, p34, "right overarm");
    right_overarm.set_cube_pos(overarm_y / 2, overarm_x / 2, 0);
    right_overarm.set_cube_size(overarm_x, overarm_y, z_depth, cube_scale);
    right_underarm = new RagdollBone(true);
    right_underarm.init(ragdoll, p36, p38, p35, p37, "right underarm");
    right_underarm.set_cube_pos(underarm_x / 2, underarm_y / 2, 0);
    right_underarm.set_cube_size(overarm_x, overarm_y, z_depth, cube_scale);
    right_hand = new RagdollBone(true);
    right_hand.init(ragdoll, p38, p40, p37, p39, "right hand");
    right_hand.set_cube_pos(hand_x / 2, hand_y / 2, 0);
    right_hand.set_cube_size(hand_x, hand_y, z_depth, cube_scale);
#if true
    left_hand.set_fixed(true);
#endif

    head_to_neck = new MyHingeJoint();
    head_to_neck.init(ragdoll, head, neck, p3, p4, 45, 45);
    neck_to_chest = new MyBallJoint();
    neck_to_chest.init(ragdoll, neck, chest, p6, p5, p7, 45, new Vector3(0, 0, 1));
    chest_to_hip = new MyBallJoint();
    chest_to_hip.init(ragdoll, chest, hip, p7, p6, p10, 45, new Vector3(0, 0, 1));
    left_hip_to_thigh = new MyBallJoint();
    left_hip_to_thigh.init(ragdoll,  left_thigh, hip, p11, p13, p10, 90, new Vector3(0, 0, 1));
    left_thigh_to_underleg = new MyHingeJoint();
    left_thigh_to_underleg.init(ragdoll, left_thigh, left_underleg, p15, p16, 45, 45);
    left_underleg_to_feet = new MyHingeJoint();
    left_underleg_to_feet.init(ragdoll, left_underleg, left_feet, p19, p20, 90, 0);
    right_hip_to_thigh = new MyBallJoint();
    right_hip_to_thigh.init(ragdoll, right_thigh, hip, p12, p14, p10, 90, new Vector3(0, 0, 1));
    right_thigh_to_underleg = new MyHingeJoint();
    right_thigh_to_underleg.init(ragdoll, right_thigh, right_underleg, p17, p18, 45, 45);
    right_underleg_to_feet = new MyHingeJoint();
    right_underleg_to_feet.init(ragdoll, right_underleg, right_feet, p21, p22, 90, 0);
    left_chest_to_overarm = new MyBallJoint();
    left_chest_to_overarm.init(ragdoll, chest, left_overarm, p8, p9, p27, 90, new Vector3(-1, 0, 0));
    left_overarm_to_underarm = new MyHingeJoint();
    left_overarm_to_underarm.init(ragdoll, left_overarm, left_underarm, p28, p29, 180, 0);
    left_arm_to_hand = new MyHingeJoint();
    left_arm_to_hand.init(ragdoll, left_underarm, left_hand, p30, p31, 90, 90);
    right_chest_to_overarm = new MyBallJoint();
    right_chest_to_overarm.init(ragdoll, chest, right_overarm, p9, p8, p34, 90, new Vector3(1, 0, 0));
    right_overarm_to_underarm = new MyHingeJoint();
    right_overarm_to_underarm.init(ragdoll, right_overarm, right_underarm, p35, p36, 180, 0);
    right_arm_to_hand = new MyHingeJoint();
    right_arm_to_hand.init(ragdoll, right_underarm, right_hand, p37, p38, 90, 90);
  }

  // Update is called once per frame
  void Update()
  {
    ragdoll.run(new Vector3(0, -9, 0), 0.005f);
  }
}
